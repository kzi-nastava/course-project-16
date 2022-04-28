using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Projekat.Users;
using System.IO;
namespace Usi_Projekat.Manage

{
    public class PatientManager
    {
        private string _patientFileName;
        private List<Patient> _patients;
        private Factory manager;

        public PatientManager()
        {
            _patients = new List<Patient>();
        }
        public PatientManager(string patientFile, Factory manager)
        {
            _patientFileName = patientFile;
            this.manager = manager;
        }
        public void loadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(_patientFileName), json);
        }
        
    }
}