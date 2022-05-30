using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace upload
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string folder, branchname, branchid;

            folder = Environment.GetEnvironmentVariable("DATA_FOLDER");
            if (string.IsNullOrEmpty(folder))
            {
                Console.WriteLine("Escriba la direccion de la carpeta data: \n");
                folder = Console.ReadLine();
                Environment.SetEnvironmentVariable("DATA_FOLDER", folder);
            }

            branchname = Environment.GetEnvironmentVariable("BRANCHNAME");
            if (string.IsNullOrEmpty(branchname))
            {
                Console.WriteLine("Escriba el nombre de la sucursal: \n");
                branchname = Console.ReadLine();
                Environment.SetEnvironmentVariable("BRANCHNAME", branchname);
            }

            branchid = Environment.GetEnvironmentVariable("BRANCHID");
            if (string.IsNullOrEmpty(branchid))
            {
                Console.WriteLine("Escriba el codigo de la sucursal: \n");
                branchid = Console.ReadLine();
                Environment.SetEnvironmentVariable("BRANCHID", branchid);
            }

            var _farmaDbContextp = new FarmaDbContext(
                $"Provider = VFPOLEDB.1; Data Source = {folder}; Collating Sequence = general;"
                );

            var query = $"select " +
                    $"transform(e.refprod,'@zl ######')+transform(e.refpres,'@zl ##') as ProductCode, " +
                    $"'{branchid}' BranchId, " +
                    $"'{branchname}' BranchName, " +
                    $"p.nomblargo ProductName, " +
                    $"p.ultpco ProductCost, " +
                    $"round(e.bcefinal,4) Existence, " +
                    $"p.factorcont2 ProductFactor, " +
                    $"p.unidad2 ProductUnit, " +
                    $"CAST(nivelmax as int)MaxStock " +
                    $"FROM inv_existencias e INNER JOIN inv_presentaciones p ON p.refprod = e.refprod AND p.refpres = e.refpres AND numalm=1 WHERE e.refpres<100 and e.refpres>0 and !deleted() ORDER BY e.refprod, e.refpres";

            IEnumerable<ProductExistenceUpdateDto> result = null;

            Console.WriteLine("Capturando Info");

            do
            {
                result = _farmaDbContextp.Get(query).Result.ToList<ProductExistenceUpdateDto>();
            } while (result == null);


            if (result == null)
            {
                Console.Write("vacio");
                Console.ReadLine();
            }
            Console.WriteLine(result.Count().ToString()+" Registros");

            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            var httpClient = new RestClient
            {
                BaseUrl = new Uri($"https://192.168.10.48:5002/api/fullstock?branchId={branchid}&branchName={branchname}"),
                //BaseUrl = new Uri($@"https://localhost:44329/api/fullstock?branchId={branchid}&branchName={branchname}"),
            };

            httpClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(result, "application/json");
            request.Timeout = -1;

            try
            {
                Console.WriteLine("PETICION ENVIADA: \n");
                IRestResponse responseResult = httpClient.Execute(request);

                if (responseResult.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(responseResult.Content + " " + responseResult.StatusCode+" "+responseResult.ErrorMessage, responseResult.ErrorException);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
