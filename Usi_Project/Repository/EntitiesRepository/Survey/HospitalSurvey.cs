namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class HospitalSurvey
    {
        public string PatientEmail;
        public int QualityOfService;
        public int OverallHygiene;
        public int WouldYouRecommend;
        public string Comment;

        public HospitalSurvey(string patientEmail, int qualityOfService, int overallHygiene, 
            int wouldYouRecommend, string comment)
        {
            PatientEmail = patientEmail;
            QualityOfService = qualityOfService;
            OverallHygiene = overallHygiene;
            WouldYouRecommend = wouldYouRecommend;
            Comment = comment;
        }

        public string PatientEmail1
        {
            get => PatientEmail;
            set => PatientEmail = value;
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

        public int WouldYouRecommend1
        {
            get => WouldYouRecommend;
            set => WouldYouRecommend = value;
        }

        public override string ToString()
        {
            return "Patient: " + PatientEmail + "\nQuality: " + QualityOfService + "\nHygiene: " + OverallHygiene 
                + "\nRecommendation: " + WouldYouRecommend + "\nComment: " + Comment;
        }
    }
}