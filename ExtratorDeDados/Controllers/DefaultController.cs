using System.Collections.Generic;
using System.Web.Http;
using ExtratorDeDados.Importer;

namespace ExtratorDeDados.Controllers
{
    public class DefaultController : ApiController
    {
        private IImportFile _importFile;

        public DefaultController(IImportFile importFile)
        {
            _importFile = importFile;
        }

        // GET: api/Default
        public IEnumerable<string> Get()
        {
            const string path = @"D:\Desenvolvimento\Arquivos\";
            _importFile.Import(path);
            
            return new string[] { "value1", "value2" };
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
