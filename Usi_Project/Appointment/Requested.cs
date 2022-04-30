using System;

namespace Usi_Project.Appointments
{
    public class Requested
    {
        private string emailPatient;
        private DateTime startTime;
        private DateTime newTime;
        private string operation;
        
        public Requested(){}
        
        public Requested(string emailPatient, DateTime startTime, DateTime newTime, string operation)
        {
            this.emailPatient = emailPatient;
            this.startTime = startTime;
            this.newTime = newTime;
            this.operation = operation;
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

        public DateTime NewTime
        {
            get => newTime;
            set => newTime = value;
        }

        public string Operation
        {
            get => operation;
            set => operation = value;
        }
        public void SaveRequest(){}
    }
    
}