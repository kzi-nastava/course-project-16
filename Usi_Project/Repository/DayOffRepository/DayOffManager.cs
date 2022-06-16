using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Repository;
using Usi_Project.Appointments;

namespace Usi_Project.Repository
{
    public class DaysOffRepository
    {
        private string _dayOffFilename;
        private List<DayOffRequest> _daysOff;
        private Factory _manager;

        public DaysOffRepository(string dayOffFilename, Factory manager)
        {
            _dayOffFilename = dayOffFilename;
            _manager = manager;
        }

        public DaysOffRepository()
        {
        }
        public DaysOffRepository(List<DayOffRequest> daysOff)
        {
            _daysOff = daysOff;
        }

        public DaysOffRepository(string dayOffFilename, List<DayOffRequest> daysOff, Factory manager)
        {
            _dayOffFilename = dayOffFilename;
            _daysOff = daysOff;
            _manager = manager;
        }

        public string DayOffFilename
        {
            get => _dayOffFilename;
            set => _dayOffFilename = value;
        }

        public List<DayOffRequest> DaysOff
        {
            get => _daysOff;
            set => _daysOff = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _daysOff = JsonConvert.DeserializeObject<List<DayOffRequest>>(File.ReadAllText(_dayOffFilename), json);

        }

        public void serialize()
        {
            using (StreamWriter file = File.CreateText(_dayOffFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _manager);
            }
        }
    }
}