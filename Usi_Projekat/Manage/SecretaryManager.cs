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

        public Secretary checkPersonalInfo(string email, string password)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email && password == secretary.password) 
                {
                    return secretary;
                }
               
            }
            return null;
        }
        
        public bool checkEmail(string email)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email)
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool checkPassword(string password)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (password == secretary.password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}