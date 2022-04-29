using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Projekat.Users;
using System.IO;
namespace Usi_Projekat.Manage
{
    public class DoctorManager
    {
        private string _doctorFilename;
        private List<Doctor> _doctors;
        private Factory _manager;

        public DoctorManager(string doctorFilename, Factory manager)
        {
            _doctorFilename = doctorFilename;
            _manager = manager;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(_doctorFilename), json);

        }
        public Doctor CheckPersonalInfo(string email, string password)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email && password == doctor.password)
                {
                    return doctor;
                }
            }
            return null;
        }
        
        public bool CheckEmail(string email)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}