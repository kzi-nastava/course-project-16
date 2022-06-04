using System.IO;

namespace Usi_Project.Settings
{
    public class FileSettings
    {
        private readonly string _doctorFilename;
        private readonly string _patientFilename;
        private readonly string _directorFilename;
        private readonly string _secretaryFilename;
        private readonly string _operatingRoomsFn;
        private readonly string _overviewRoomsFn;
        private readonly string _retiringRoomsFn;
        private readonly string _appointmentsFn;
        private readonly string _anamnesaFn;
        private readonly string _requestedFn;
        private readonly string _stockRoomFn;
        private readonly string _timerFn;
        private readonly string _recipesFn;
        private readonly string _drugsFn;
        private readonly string _rejectedDrugsFn;
        public FileSettings(string doctorFilename, string patientFilename, string directorFilename,
            string secretaryFilename, string operatingRoomsFn, string overviewRoomsFn, string retiringRoomsFn,
            string appointmentsFn, string anamnesaFn, string requestedFn, 
            string stockRoomFn, string timerFn, string recipesFn, string drugsFn, string rejectedDrugsFn)
        
        {
            _doctorFilename = doctorFilename;
            _patientFilename = patientFilename;
            _directorFilename = directorFilename;
            _secretaryFilename = secretaryFilename;
            _operatingRoomsFn = operatingRoomsFn;
            _overviewRoomsFn = overviewRoomsFn;
            _appointmentsFn = appointmentsFn;
            _retiringRoomsFn = retiringRoomsFn;
            _anamnesaFn = anamnesaFn;
            _requestedFn = requestedFn;
            _stockRoomFn = stockRoomFn;
            _timerFn = timerFn;
            _recipesFn = recipesFn;
            _drugsFn = drugsFn;
            _rejectedDrugsFn = rejectedDrugsFn;

        }

        public string DrugsFn => _drugsFn;

        public string RecipesFn => _recipesFn;

        public string RequestedFn => _requestedFn;
        
        public string RejectedDrugsFn => _rejectedDrugsFn;

        public string AnamnesaFn => _anamnesaFn;
    

        public string TimerFn => _timerFn;

        public string OperatingRoomsFn => _operatingRoomsFn;

        public string OverviewRoomsFn => _overviewRoomsFn;

        public string RetiringRoomsFn => _retiringRoomsFn;

        public string AppointmentsFn => _appointmentsFn;

        public string DoctorFilename => _doctorFilename;

        public string PatientFilename => _patientFilename;

        public string DirectorFilename => _directorFilename;

        public string StockRoomFn => _stockRoomFn;

        public string SecretaryFilename => _secretaryFilename;
    }
}