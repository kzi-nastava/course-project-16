using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Projekat.Users;
using System.IO;
namespace Usi_Projekat.Manage
{
    public class DirectorManager
    {
        private string _directorFilename;
        private Director _director;
        private Factory _manager;

        public DirectorManager(string directorFilename, Factory factory)
        {
            _directorFilename = directorFilename;
            _manager = factory;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _director = JsonConvert.DeserializeObject<Director>(File.ReadAllText(_directorFilename), json);
        }
        public Director CheckPersonalInfo(string email, string password)
        {
            if (email == _director.email && password == _director.password)
            {
                return _director;
            }
            return null;
        }
        public bool CheckEmail(string email)
        {
            return (email == _director.email);
        }
    }
}