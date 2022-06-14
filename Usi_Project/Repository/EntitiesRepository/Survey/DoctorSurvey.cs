using System;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class DoctorSurvey
    {
        public DateTime startTime;
        public DateTime endTime;
        public string patientEmail;
        public string doctorEmail;
        public int qualityOfService;
        public int overallHygiene;
        public int areYouSatisfied;
        public int wouldYouRecommend;
        public string comment;

        public DoctorSurvey(DateTime startTime, DateTime endTime, string patientEmail, string doctorEmail, 
            int qualityOfService, int overallHygiene, int areYouSatisfied, int wouldYouRecommend, string comment)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.patientEmail = patientEmail;
            this.doctorEmail = doctorEmail;
            this.qualityOfService = qualityOfService;
            this.overallHygiene = overallHygiene;
            this.areYouSatisfied = areYouSatisfied;
            this.wouldYouRecommend = wouldYouRecommend;
            this.comment = comment;
        }

        
        public override string ToString()
        {
            return "Patient: " + patientEmail + "\nDoctor: "+ doctorEmail +"\nAppointment Made: " + startTime 
                   + "\nAppointment Finished: " + endTime + "\nQuality: " + qualityOfService + "\nHygiene: "
                   + overallHygiene + "\nAre You Satisfied: " + areYouSatisfied + "\nRecommendation: "  +
                   + wouldYouRecommend + "\nComment: " + comment;

        }

    }
}