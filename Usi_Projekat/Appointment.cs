using System;

namespace Usi_Projekat
{
    public class Appointment
    {
        public string emailDoctor;
        public string emailPatient;
        public DateTime startTime;
        public DateTime endTime;
        public string type;
        public OperatingRoom operatingRoom;
        public OverviewRoom overviewRoom;

        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OperatingRoom operatingRoom)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OP";
            this.operatingRoom = operatingRoom;
        }
        public Appointment(string emailDoctor, string emailPatient, DateTime startTime, DateTime endTime, string type, OverviewRoom overviewRoom)
        {
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = "OV";
            this.overviewRoom = overviewRoom;
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

        public OperatingRoom OperatingRoom
        {
            get => operatingRoom;
            set => operatingRoom = value;
        }

        public OverviewRoom OverviewRoom
        {
            get => overviewRoom;
            set => overviewRoom = value;
        }

        
    }
}