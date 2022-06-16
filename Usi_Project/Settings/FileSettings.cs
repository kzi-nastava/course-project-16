using System.IO;

namespace Usi_Project.Settings
{
    public class FileSettings
    {
        private readonly string _doctorFilename;
        private readonly string _patientFilename;
        private readonly string _directorFilename;
        private readonly string _secretaryFilename;
        private readonly string _operatingRoomsFilename;
        private readonly string _overviewRoomsFilename;
        private readonly string _retiringRoomsFilename;
        private readonly string _appointmentsFilename;
        private readonly string _anamnesaFilename;
        private readonly string _requestedFilename;
        private readonly string _stockRoomFilename;
        private readonly string _timerFilename;
        private readonly string _recipesFilename;
        private readonly string _dynamicReqFilename;
        private readonly string _drugsFilename;
        private readonly string _rejectedDrugsFilename;
        private readonly string _hospitalSurveyFilename;
        private readonly string _doctorSurveyFilename;
        private readonly string _dayOffRequestsFilename;
        private readonly string _notificationFilename;
        public FileSettings(string doctorFilename, string patientFilename, string directorFilename,
            string secretaryFilename, string operatingRoomsFilename, string overviewRoomsFilename, 
            string retiringRoomsFilename, string appointmentsFilename, string anamnesaFilename, 
            string requestedFilename, string stockRoomFilename, string timerFilename, string recipesFilename, 
            string dynamicReqFilename, string drugsFilename, string rejectedDrugsFilename, 
            string hospitalSurveyFilename, string doctorSurveyFilename,string dayOffRequestsFilename, string notificationFilename)
        {
            _doctorFilename = doctorFilename;
            _patientFilename = patientFilename;
            _directorFilename = directorFilename;
            _secretaryFilename = secretaryFilename;
            _operatingRoomsFilename = operatingRoomsFilename;
            _overviewRoomsFilename = overviewRoomsFilename;
            _appointmentsFilename = appointmentsFilename;
            _retiringRoomsFilename = retiringRoomsFilename;
            _anamnesaFilename = anamnesaFilename;
            _requestedFilename = requestedFilename;
            _stockRoomFilename = stockRoomFilename;
            _timerFilename = timerFilename;
            _recipesFilename = recipesFilename;
            _dynamicReqFilename = dynamicReqFilename;
            _drugsFilename = drugsFilename;
            _rejectedDrugsFilename = rejectedDrugsFilename;
            _hospitalSurveyFilename = hospitalSurveyFilename;
            _doctorSurveyFilename = doctorSurveyFilename;
            _dayOffRequestsFilename = dayOffRequestsFilename;
            _notificationFilename = notificationFilename;
        }

        public string NotificationFilename => _notificationFilename;
        public string DayOffRequestsFilename => _dayOffRequestsFilename;
        public string DoctorSurveyFilename => _doctorSurveyFilename;

        public string HospitalSurveyFilename => _hospitalSurveyFilename;

        public string DrugsFilename => _drugsFilename;

        public string RecipesFilename => _recipesFilename;

        public string RequestedFilename => _requestedFilename;
        
        public string RejectedDrugsFilename => _rejectedDrugsFilename;

        public string AnamnesaFilename => _anamnesaFilename;
        
        public string TimerFilename => _timerFilename;

        public string OperatingRoomsFilename => _operatingRoomsFilename;

        public string OverviewRoomsFilename => _overviewRoomsFilename;

        public string RetiringRoomsFilename => _retiringRoomsFilename;

        public string AppointmentsFilename => _appointmentsFilename;

        public string DoctorFilename => _doctorFilename;

        public string PatientFilename => _patientFilename;

        public string DirectorFilename => _directorFilename;

        public string StockRoomFilename => _stockRoomFilename;

        public string SecretaryFilename => _secretaryFilename;
        
        public string DynamicReqFilename => _dynamicReqFilename;
    }
}