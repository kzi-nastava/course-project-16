using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Usi_Project.Appointments;


namespace Usi_Project.Manage
{
    public class RequestManager
    {
        private string _requestFilename;
        private List<Requested> _requested;
        private Factory _manager;

        public RequestManager() {}
        public RequestManager(string requestFilename, Factory manager)
        {
            _requestFilename = requestFilename;
            _requested = new List<Requested>();
            _manager = manager;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            _requested = JsonConvert.DeserializeObject<List<Requested>>(File.ReadAllText(_requestFilename), json);
        }

        public string RequestFilename
        {
            get => _requestFilename;
            set => _requestFilename = value;
        }

        public List<Requested> Requested
        {
            get => _requested;
            set => _requested = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
        public void Serialize(Requested request)
        {
            using (StreamWriter file = File.CreateText(_requestFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, request);
            }
        }
    }
}