using System;
using Newtonsoft.Json;
using Usi_Project.Manage;

namespace Usi_Project.Appointments
{
    public class Appointment
    {
        private string emailDoctor;
        private string emailPatient;
        private DateTime startTime;
        private DateTime endTime;
        private string type;
        private OperatingRoom operatingRoom;
        private OverviewRoom overviewRoom;
        private string idRoom;

        public Appointment()
        {
        }

        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, string idRoom)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = type;
            this.idRoom = idRoom;
        }

        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OperatingRoom operatingRoom)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OP";
            this.idRoom = operatingRoom.Id;
        }
        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OverviewRoom overviewRoom)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OV";
            this.idRoom = overviewRoom.Id;
        }
        
        
        public string IdRoom
        {
            get => idRoom;
            set => idRoom = value;
        }
        public string EmailDoctor
        {
            get => emailDoctor;
            set => emailDoctor = value;
        }

        public string EmailPatient
        {
            get => emailPatient;
            set => emailPatient = value;
        }

        public DateTime StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        public DateTime EndTime
        {
            get => endTime;
            set => endTime = value;
        }

        public string Type
        {
            get => type;
            set => type = value;
        }

        [JsonIgnore]
        public OperatingRoom OperatingRoom
        {
          
            get => operatingRoom;
            set => operatingRoom = value;
        }

        [JsonIgnore]
        public OverviewRoom OverviewRoom
        {
            get => overviewRoom;
            set => overviewRoom = value;
        }

        
    }
}