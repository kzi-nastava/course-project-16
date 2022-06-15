using System.IO;
using Newtonsoft.Json;
using Usi_Project.Settings;
using Newtonsoft.Json;
using Usi_Project.DataSaver;
using Usi_Project.Manage;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.roomRepository.EntitiesRepository.DirectorRepository;

namespace Usi_Project.Repository
{
    public class Factory
    {

        private DynamicRequestManager _dynamicRequestManager;
        private readonly DirectorManager _directorManager;
        private readonly PatientManager _patientManager;
        private readonly SecretaryManager _secretaryManager;
        private readonly DoctorManager _doctorManager;
        private readonly RoomRepository _roomRepository;
        private readonly AppointmentManager _appointmentManager;
        private readonly AnamnesaManager _anamnesaManager;
        private readonly RequestManager _requestManager;
        private readonly TimerManager _timerManager;
        private readonly Saver _saver;
        private readonly RecipesManager _recipesManager;
        private readonly DrugRepository _drugRepository;
        private readonly HospitalSurveyManager _hospitalSurveyManager;
        private readonly DoctorSurveyManager _doctorSurveyManager;
        public Factory()
        {
        }
        public Factory(FileSettings fileSettings, Saver saver)

        {
            _patientManager = new PatientManager(fileSettings.PatientFilename, this);
            _secretaryManager = new SecretaryManager(fileSettings.SecretaryFilename, this);
            _doctorManager = new DoctorManager(fileSettings.DoctorFilename, this);
            _roomRepository = new RoomRepository(fileSettings);
            _appointmentManager = new AppointmentManager(fileSettings.AppointmentsFilename, this);
            _anamnesaManager = new AnamnesaManager(fileSettings.AnamnesaFilename, this);
            _requestManager = new RequestManager(fileSettings.RequestedFilename, this);
            _saver = saver;
            _timerManager = new TimerManager(fileSettings.TimerFilename, this);
            _recipesManager = new RecipesManager(fileSettings.RecipesFilename, this);
            _dynamicRequestManager = new DynamicRequestManager(fileSettings.DynamicReqFilename, this);
            _drugRepository = new DrugRepository(fileSettings.DrugsFilename, fileSettings.RejectedDrugsFilename);
            _hospitalSurveyManager = new HospitalSurveyManager(fileSettings.HospitalSurveyFilename, this);
            _doctorSurveyManager = new DoctorSurveyManager(fileSettings.DoctorSurveyFilename, this);
            _directorManager = new DirectorManager(fileSettings.DirectorFilename, _roomRepository, _hospitalSurveyManager, _doctorSurveyManager, _timerManager, _drugRepository, _doctorManager);

        }
        
        public void LoadData()
        {

            _directorManager.LoadData();
            _patientManager.LoadData();
            _secretaryManager.LoadData();
            _doctorManager.LoadData();
            _roomRepository.LoadData();
            _appointmentManager.LoadData();
            _anamnesaManager.LoadData();
            _requestManager.LoadData();
            _timerManager.LoadData();
            _dynamicRequestManager.LoadData();
            _drugRepository.LoadData();
            _recipesManager.LoadData();
            _hospitalSurveyManager.LoadData();
            _doctorSurveyManager.LoadData();
        }
        
        public TimerManager TimerManager
        {
            get => _timerManager;

        }

        public HospitalSurveyManager HospitalSurveyManager => _hospitalSurveyManager;

        public RoomRepository RoomRepository
        {
            get => _roomRepository;
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

        public DynamicRequestManager DynamicRequestManager
        {
            get => _dynamicRequestManager;
            set => _dynamicRequestManager = value;
        }

        public AppointmentManager AppointmentManager => _appointmentManager;

        public AnamnesaManager AnamnesaManager => _anamnesaManager;

        public RequestManager RequestManager => _requestManager;

        public RecipesManager RecipesManager => _recipesManager;

        public DoctorSurveyManager DoctorSurveyManager => _doctorSurveyManager;

        public DrugRepository DrugRepository => _drugRepository;
        
        public Saver Saver
        {
            get => _saver;
        }
    }

}