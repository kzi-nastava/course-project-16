using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project
{
    public class Notification
    {
        public DateTime sdate;
        public DateTime edate;
        public string reason;
        public string verification;
        public string doctorEmail;

        public Notification(DateTime sdate, DateTime edate, string reason, string verification, string doctorEmail)
        {
            this.sdate = sdate;
            this.edate = edate;
            this.reason = reason;
            this.verification = verification;
            this.doctorEmail = doctorEmail;
        }
    }
}