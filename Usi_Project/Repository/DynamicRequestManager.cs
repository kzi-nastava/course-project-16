using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Usi_Project.Appointments;
using Usi_Project.Repository;


namespace Usi_Project.Manage
{
    public class DynamicRequestManager
    {
        private string _requestFilename;
        private List<DynamicRequest> _dynamicRequests;
        private Factory _manager;

        public DynamicRequestManager(string requestFilename, Factory manager)
        {
            _requestFilename = requestFilename;
            _dynamicRequests = new List<DynamicRequest>();
            _manager = manager;
        }
        
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            _dynamicRequests = JsonConvert.DeserializeObject<List<DynamicRequest>>
                (File.ReadAllText(_requestFilename), json);
        }

        public string RequestFilename
        {
            get => _requestFilename;
            set => _requestFilename = value;
        }

        public List<DynamicRequest> DynamicRequests
        {
            get => _dynamicRequests;
            set => _dynamicRequests = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
    }
}