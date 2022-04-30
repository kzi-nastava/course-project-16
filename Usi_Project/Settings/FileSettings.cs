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

        public FileSettings(string doctorFilename, string patientFilename, string directorFilename, 
            string secretaryFilename,string operatingRoomsFn, string overviewRoomsFn, string retiringRoomsFn, string appointmentsFn,string anamnesaFn
            ,string requestedFn)
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

        }

        public string RequestedFn => _requestedFn;

        public string AnamnesaFn => _anamnesaFn;

        public string OperatingRoomsFn => _operatingRoomsFn;

        public string OverviewRoomsFn => _overviewRoomsFn;

        public string RetiringRoomsFn => _retiringRoomsFn;

        public string AppointmentsFn => _appointmentsFn;

        public string DoctorFilename => _doctorFilename;

        public string PatientFilename => _patientFilename;

        public string DirectorFilename => _directorFilename;

        public string SecretaryFilename => _secretaryFilename;
    }
}