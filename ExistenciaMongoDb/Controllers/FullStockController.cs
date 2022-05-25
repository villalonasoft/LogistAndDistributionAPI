using ExistenciaMongoDb.Model;
using ExistenciaMongoDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullStockController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMemoryCache _memoryCache;

        public FullStockController(MongoDbService mongoDbService, IMemoryCache memoryCache)
        {
            _mongoDbService = mongoDbService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var result = await _mongoDbService.GetCollections();
            return Ok(result.OrderBy(x => Convert.ToInt32(x.Split('-')[0])));
        }

        //[HttpGet("{collectionName}")]
        //public async Task<IActionResult> GetCollections(string collectionName)
        //{
        //    var result = await _mongoDbService.GetList(collectionName);
        //    return Ok(result.OrderBy(x=>x.ProductName));
        //}

        [HttpGet("masive")]
        public async Task<IActionResult> Masive()
        {
            if (!_memoryCache.TryGetValue("inventario", out List<Existencia> fullStock))
            {
                fullStock = await _mongoDbService.GetList(); // Get the data from database
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(12),
                    SlidingExpiration = TimeSpan.FromHours(8),
                    Size = 1024,
                };
                _memoryCache.Set("inventario", fullStock, cacheEntryOptions);
            }
            return Ok(fullStock);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var path = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
            if (System.IO.File.Exists(path))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(path, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                return File(bytes, contentType, Path.GetFileName(path));
            }

            _memoryCache.TryGetValue("inventario", out List<Existencia> fullStock);
            if (fullStock == null)
            {
                return BadRequest();
            }
            DataTable table = (DataTable)JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(
                    fullStock,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }),
                (typeof(DataTable)));
            
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");
                List<String> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                    columnIndex++;
                }
                int rowIndex = 1;
                foreach (DataRow dsrow in table.Rows)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (String col in columns)
                    {
                        row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                        cellIndex++;
                    }

                    rowIndex++;
                }
                workbook.Write(fs);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(path, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                return File(bytes, contentType, Path.GetFileName(path));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string branchId,string branchName, List<FullStock> postDtos)
        {
            await _mongoDbService.CreateAsync(branchId,branchName, postDtos);
            return Ok();
        }
    }
}
