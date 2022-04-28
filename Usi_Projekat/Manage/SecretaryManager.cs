using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Projekat.Users;
using System.IO;


namespace Usi_Projekat.Manage
{
    public class SecretaryManager
    {
        private string _secretaryFilename;
        private List<Secretary> _secretaries;
        private Factory _manager;

        public SecretaryManager(string secretaryFilename, Factory factory)
        {
            _secretaryFilename = secretaryFilename;
            _manager = factory;
        }
        public void  loadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(_secretaryFilename), json);

        }
    }
}