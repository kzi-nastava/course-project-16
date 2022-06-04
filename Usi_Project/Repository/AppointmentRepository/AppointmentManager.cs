using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Project.Users;
using System.IO;
using Usi_Project.Appointments;

namespace Usi_Project.Repository
{
    public class AppointmentManager
    {
        private string _appointmentFilename;
        private List<Appointment> _appointment;
        private Factory _manager;

        public AppointmentManager()
        {
            _appointment = new List<Appointment>();
        }

        public AppointmentManager(string appointmentFilename, List<Appointment> appointment, Factory manager)
        {
            _appointmentFilename = appointmentFilename;
            _appointment = appointment;
            _manager = manager;
        }

        public AppointmentManager(string appointmentFilename, Factory manager)
        {
            _appointmentFilename = appointmentFilename;
            _appointment = new List<Appointment>();
            _manager = manager;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _appointment = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(_appointmentFilename), json);

        }

        public void serialize()
        {
            using (StreamWriter file = File.CreateText(_appointmentFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _appointment);
            }
        }
        public string AppointmentFilename
        {
            get => _appointmentFilename;
            set => _appointmentFilename = value;
        }

        public List<Appointment> Appointment
        {
            get => _appointment;
            set => _appointment = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
    }
}