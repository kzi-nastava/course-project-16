using System;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class DoctorSurvey
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string PatientEmail;
        public string DoctorEmail;
        public int QualityOfService;
        public int OverallHygiene;
        public int AreYouSatisfied;
        public int WouldYouRecommend;
        public string Comment;

        public DoctorSurvey(DateTime startTime, DateTime endTime, string patientEmail, string doctorEmail, int qualityOfService, int overallHygiene, int areYouSatisfied, int wouldYouRecommend, string comment)
        {
            StartTime = startTime;
            EndTime = endTime;
            PatientEmail = patientEmail;
            DoctorEmail = doctorEmail;
            QualityOfService = qualityOfService;
            OverallHygiene = overallHygiene;
            AreYouSatisfied = areYouSatisfied;
            WouldYouRecommend = wouldYouRecommend;
            Comment = comment;
        }

        public DateTime StartTime1
        {
            get => StartTime;
            set => StartTime = value;
        }

        public DateTime EndTime1
        {
            get => EndTime;
            set => EndTime = value;
        }

        public string PatientEmail1
        {
            get => PatientEmail;
            set => PatientEmail = value;
        }

        public string DoctorEmail1
        {
            get => DoctorEmail;
            set => DoctorEmail = value;
        }

        public int QualityOfService1
        {
            get => QualityOfService;
            set => QualityOfService = value;
        }

        public int OverallHygiene1
        {
            get => OverallHygiene;
            set => OverallHygiene = value;
        }

        public int AreYouSatisfied1
        {
            get => AreYouSatisfied;
            set => AreYouSatisfied = value;
        }

        public int WouldYouRecommend1
        {
            get => WouldYouRecommend;
            set => WouldYouRecommend = value;
        }

        public string Comment1
        {
            get => Comment;
            set => Comment = value;
        }

        public override string ToString()
        {
            return "Patient: " + PatientEmail + "\nAppointment Made: " + StartTime + "\nAppointment Finished: " + EndTime 
                   + "\nQuality: " + QualityOfService + "\nHygiene: " + OverallHygiene + "\nAre You Satisfied: " +
                   AreYouSatisfied
                   + "\nRecommendation: "  +
                   + WouldYouRecommend + "\nComment: " + Comment;

        }

    }
}