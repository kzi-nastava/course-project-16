using Usi_Projekat.Manage;
using Usi_Projekat.Settings;
namespace Usi_Projekat.Manage
{
    public class Factory
    {
        private FileSettings _fileSettings;
        private DirectorManager _directorManager;
        private PatientManager _patientManager;
        private SecretaryManager _secretaryManager;
        private DoctorManager _doctorManager;

        public Factory(FileSettings fileSettings)
        {
            _fileSettings = fileSettings;
            _directorManager = new DirectorManager(_fileSettings.DirectorFilename, this);
            _patientManager = new PatientManager(_fileSettings.PatientFilename, this);
            _secretaryManager = new SecretaryManager(_fileSettings.SecretaryFilename, this);
            _doctorManager = new DoctorManager(_fileSettings.DoctorFilename, this);
        }
        public void loadData()
        {
            _directorManager.loadData();
            _patientManager.loadData();
            _secretaryManager.loadData();
            _doctorManager.loadData();
        }

        public FileSettings FileSettings
        {
            get => _fileSettings;
        }
        public DirectorManager DirectorManager
        {
            get => _directorManager;
        }
        public PatientManager PatientManager
        {
            get => _patientManager;
        }
        public SecretaryManager SecretaryManager
        {
            get => _secretaryManager;
        }
        public DoctorManager DoctorManager
        {
            get => _doctorManager; 
        }
    }
}