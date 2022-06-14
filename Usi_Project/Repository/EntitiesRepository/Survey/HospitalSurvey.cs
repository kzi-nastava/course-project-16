namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class HospitalSurvey
    {
        public string patientEmail;
        public int qualityOfService;
        public int overallHygiene;
        public int wouldYouRecommend;
        public int areYouSatisfied;
        public string comment;

        public HospitalSurvey(string patientEmail, int qualityOfService, int overallHygiene, int wouldYouRecommend, int areYouSatisfied, string comment)
        {
            this.patientEmail = patientEmail;
            this.qualityOfService = qualityOfService;
            this.overallHygiene = overallHygiene;
            this.wouldYouRecommend = wouldYouRecommend;
            this.areYouSatisfied = areYouSatisfied;
            this.comment = comment;
        }

        public override string ToString()
        {
            return "Patient: " + patientEmail + "\nQuality: " + qualityOfService + "\nHygiene: " + overallHygiene 
                + "\nRecommendation: " + wouldYouRecommend + "\nSatisfaction:"+ areYouSatisfied + "\nComment: " + comment;
        }
    }
}