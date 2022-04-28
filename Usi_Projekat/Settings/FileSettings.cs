using System.IO;

namespace Usi_Projekat.Settings
{
    public class FileSettings
    {
        private string _doctorFilename;
        private string _patientFilename;
        private string _directorFilename;
        private string _secretaryFilename;

        public FileSettings(string doctorFilename, string patientFilename, string directorFilename, string secretaryFilename)
        {
            _doctorFilename = doctorFilename;
            _patientFilename = patientFilename;
            _directorFilename = directorFilename;
            _secretaryFilename = secretaryFilename;
        }

        public string DoctorFilename
        {
            get => _doctorFilename;
        }

        public string PatientFilename
        {
            get => _patientFilename;
        }

        public string DirectorFilename
        {
            get => _directorFilename;
        }

        public string SecretaryFilename
        {
            get => _secretaryFilename;
        }
    }
}