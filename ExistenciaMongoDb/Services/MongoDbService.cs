using ExistenciaMongoDb.Dto;
using ExistenciaMongoDb.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Services
{
    public class MongoDbService
    {
        private readonly IOptions<MongoDbSettings> _mongoDbSettings;
        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
        }

        public async Task<List<String>> GetCollections()
        {
            MongoClient client = new MongoClient(_mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(_mongoDbSettings.Value.DatabaseName);
            var result = await database.ListCollectionNamesAsync();
            return await result.ToListAsync();
        }
        public async Task<List<Existencia>> GetList()
        {
            List<FullStockDTO> listaBruta = new List<FullStockDTO>();
            IMongoCollection<FullStock> _ExistenciaCollection;
            MongoClient client = new MongoClient(_mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(_mongoDbSettings.Value.DatabaseName);

            foreach (var collectionName in await GetCollections())
            {
                _ExistenciaCollection = database.GetCollection<FullStock>(collectionName);
                listaBruta.AddRange(await _ExistenciaCollection.Find(_ => true).Project(new ProjectionDefinitionBuilder<FullStock>()
                    .Expression(x =>
                    new FullStockDTO
                    {
                        Sucursal = x.BranchName,
                        Nombre = x.ProductName,
                        Codigo = x.ProductCode,
                        Existencia = x.Existence / x.ProductFactor,
                        NivelMax = x.MaxStock
                    }))
                    .ToListAsync());
            }
            var matriz = new List<Existencia>();
            var codes = listaBruta.GroupBy(x => x.Codigo).Select(x=> new Listacodigo{ Code=x.Key,Name=x.Select(x=>x.Nombre).FirstOrDefault()}).ToList();
            var codesSplit = codes.Select((x, i) => new { Index = i, Value = x }).GroupBy(x => x.Index / 4000).Select(x => x.Select(v => v.Value).ToList()).ToList();
            var task = new List<Task>();
            foreach(var colist in codesSplit)
            {
                var t = new Task(() =>
                {
                    matriz.AddRange(CrearMatriz(colist, listaBruta));
                });
                task.Add(t);
                t.Start();
            }
            Task.WaitAll(task.ToArray());
            return matriz;
        }

        private List<Existencia> CrearMatriz(List<Listacodigo> listacodigos, List<FullStockDTO> listaBruta)
        {
            List<Existencia> matriz = new List<Existencia>();
            Type temp = typeof(Existencia);
            foreach (var co in listacodigos)
            {
                Existencia obj = Activator.CreateInstance<Existencia>();
                var valores = listaBruta.Where(x => x.Codigo == co.Code).ToList();
                var columns = temp.GetProperties()
                    .Where(x => valores.Any(y => y.Sucursal.Split('-')[0].Replace(" ", "").ToUpper() == x.Name.ToUpper()) || x.Name == "Code" || x.Name== "ProductName" || x.Name.Contains("Max"))
                    .ToList();
                foreach (PropertyInfo pro in columns)
                {
                    if (pro.Name == "Code")
                    {
                        pro.SetValue(obj, co.Code, null);
                    }
                    else if(pro.Name == "ProductName")
                    {
                        pro.SetValue(obj, co.Name, null);
                    }
                    else if (pro.Name.Contains("Max"))
                    {
                        var row = valores.FirstOrDefault(x => x.Sucursal.Split('-')[0].Replace(" ", "").Replace("Max","").ToUpper().Trim().ToUpper() == pro.Name.Replace("Max","").Trim().ToUpper());
                        pro.SetValue(obj,
                            row?.NivelMax
                            , null);
                    }
                    else
                    {
                        var row = valores.FirstOrDefault(x => x.Sucursal.Split('-')[0].Replace(" ", "").ToUpper().Trim().ToUpper() == pro.Name.Trim().ToUpper());
                        pro.SetValue(obj,
                            row?.Existencia
                            , null);
                    }
                }
                matriz.Add(obj);
            }
            return matriz;
        }

        public async Task CreateAsync(string branchId, string branchName, List<FullStock> fullStock)
        {
            IMongoCollection<FullStock> _ExistenciaCollection;
            MongoClient client = new MongoClient(_mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(_mongoDbSettings.Value.DatabaseName);
            _ExistenciaCollection = database.GetCollection<FullStock>($"{branchId}-{branchName}");
            _ExistenciaCollection.Database.DropCollection($"{branchId}-{branchName}");
            await _ExistenciaCollection.InsertManyAsync(fullStock);
            return;
        }
    }
    internal class Listacodigo
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
