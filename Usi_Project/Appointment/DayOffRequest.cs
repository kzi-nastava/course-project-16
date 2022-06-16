using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project.Appointments
{
    public class DayOffRequest
    {
        public string doctorEmail;
        public DateTime sdate;
        public DateTime edate;
        public string reason;
        public string emergency;
        public string verification;

        public DayOffRequest()
        {
        }

        public DayOffRequest(string doctorEmail, DateTime sdate, DateTime edate, string reason, string emergency,string verification)
        {
            this.doctorEmail = doctorEmail;
            this.sdate = sdate;
            this.edate = edate;
            this.reason = reason;
            this.emergency = emergency;
            this.verification = verification;
        }

       
    }
}