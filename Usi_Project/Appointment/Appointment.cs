using System;
using Newtonsoft.Json;
using Usi_Project.Repository;

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
        private string status;

        public Appointment()
        {
        }

        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, string idRoom, string status)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = type;
            this.idRoom = idRoom;
            this.status = status;
        }

    

        public void PrintAppointment()
        {
            Console.WriteLine(this.EmailDoctor);
            Console.WriteLine(this.EmailPatient);
            Console.WriteLine(this.StartTime);
            Console.WriteLine(this.EndTime);
            Console.WriteLine(this.Type);
            Console.WriteLine(this.IdRoom);
            Console.WriteLine(this.status);
            Console.WriteLine("---------------------");
        }

        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OperatingRoom operatingRoom,string status)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OP";
            this.idRoom = operatingRoom.Id;
            this.status = status;
        }
        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OverviewRoom overviewRoom,string status)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OV";
            this.idRoom = overviewRoom.Id;
            this.status = status;
        }

        
        public string Status
        {
            get => status;
            set => status = value;
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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}