using System.IO;
using Newtonsoft.Json;
using Usi_Project.Settings;
using Newtonsoft.Json;
using Usi_Project.DataSaver;
using Usi_Project.Manage;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.Repository.EntitiesRepository.DirectorsRepository;

namespace Usi_Project.Repository
{
    public class Factory
    {

        private DynamicRequestManager _dynamicRequestManager;
        private readonly DirectorRepository _directorRepository;
        private readonly PatientsRepository _PatientsRepository;
        private readonly SecretariesRepository _SecretariesRepository;
        private readonly DoctorsRepository _DoctorsRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AppointmentsRepository _AppointmentsRepository;
        private readonly AnamnesasRepository _AnamnesasRepository;
        private readonly RequestManager _requestManager;
        private readonly TimerManager _timerManager;
        private readonly Saver _saver;
        private readonly RecipesManager _recipesManager;
        private readonly DrugsRepository _DrugsRepository;
        private readonly HospitalSurveyManager _hospitalSurveyManager;
        private readonly DoctorsSurveyRepository _DoctorsSurveyRepository;
        private readonly DaysOffRepository _dayOffRepository;
        private readonly NotificationRepository.NotificationRepository _notificationRepository;
        public Factory()
        {
        }
        public Factory(FileSettings fileSettings, Saver saver)

        {
            _PatientsRepository = new PatientsRepository(fileSettings.PatientFilename, this);
            _SecretariesRepository = new SecretariesRepository(fileSettings.SecretaryFilename, this);
            _DoctorsRepository = new DoctorsRepository(fileSettings.DoctorFilename, this);
            _roomRepository = new RoomRepository(fileSettings);
            _AppointmentsRepository = new AppointmentsRepository(fileSettings.AppointmentsFilename, this);
            _AnamnesasRepository = new AnamnesasRepository(fileSettings.AnamnesaFilename, this);
            _requestManager = new RequestManager(fileSettings.RequestedFilename, this);
            _saver = saver;
            _timerManager = new TimerManager(fileSettings.TimerFilename, this);
            _recipesManager = new RecipesManager(fileSettings.RecipesFilename, this);
            _dynamicRequestManager = new DynamicRequestManager(fileSettings.DynamicReqFilename, this);
            _DrugsRepository = new DrugsRepository(fileSettings.DrugsFilename, fileSettings.RejectedDrugsFilename);
            _hospitalSurveyManager = new HospitalSurveyManager(fileSettings.HospitalSurveyFilename, this);
            _DoctorsSurveyRepository = new DoctorsSurveyRepository(fileSettings.DoctorSurveyFilename, this);
            _directorRepository = new DirectorRepository(fileSettings.DirectorFilename, _roomRepository, _hospitalSurveyManager,
                _DoctorsSurveyRepository, _timerManager, _DrugsRepository, _DoctorsRepository);
            _dayOffRepository = new DaysOffRepository(fileSettings.DayOffRequestsFilename, this);
            _notificationRepository =
                new NotificationRepository.NotificationRepository(fileSettings.NotificationFilename, this);
        }
        
        public void LoadData()
        {

            _directorRepository.LoadData();
            _PatientsRepository.LoadData();
            _SecretariesRepository.LoadData();
            _DoctorsRepository.LoadData();
            _roomRepository.LoadData();
            _AppointmentsRepository.LoadData();
            _AnamnesasRepository.LoadData();
            _requestManager.LoadData();
            _timerManager.LoadData();
            _dynamicRequestManager.LoadData();
            _DrugsRepository.LoadData();
            _recipesManager.LoadData();
            _hospitalSurveyManager.LoadData();
            _DoctorsSurveyRepository.LoadData();
            _dayOffRepository.LoadData();
            _notificationRepository.LoadData();
        }

        public DirectorRepository DirectorRepository1 => _directorRepository;

        public PatientsRepository PatientsRepository1 => _PatientsRepository;

        public SecretariesRepository SecretariesRepository1 => _SecretariesRepository;

        public DoctorsRepository DoctorsRepository1 => _DoctorsRepository;

        public AppointmentsRepository AppointmentsRepository1 => _AppointmentsRepository;

        public AnamnesasRepository AnamnesasRepository1 => _AnamnesasRepository;

        public DrugsRepository DrugsRepository1 => _DrugsRepository;

        public DoctorsSurveyRepository DoctorsSurveyRepository1 => _DoctorsSurveyRepository;

        public DaysOffRepository DayOffRepository => _dayOffRepository;

        public NotificationRepository.NotificationRepository NotificationRepository1 => _notificationRepository;

        public TimerManager TimerManager
        {
            get => _timerManager;

        }

        public HospitalSurveyManager HospitalSurveyManager => _hospitalSurveyManager;

        public RoomRepository RoomRepository
        {
            get => _roomRepository;
        }

        public DirectorRepository DirectorRepository
        {
            get => _directorRepository;
        }

        public PatientsRepository PatientsRepository
        {
            get => _PatientsRepository;
        }

        public SecretariesRepository SecretariesRepository
        {
            get => _SecretariesRepository;
        }

        public DoctorsRepository DoctorsRepository
        {
            get => _DoctorsRepository;
        }

        public DynamicRequestManager DynamicRequestManager
        {
            get => _dynamicRequestManager;
            set => _dynamicRequestManager = value;
        }

        public AppointmentsRepository AppointmentsRepository => _AppointmentsRepository;

        public AnamnesasRepository AnamnesasRepository => _AnamnesasRepository;

        public RequestManager RequestManager => _requestManager;

        public RecipesManager RecipesManager => _recipesManager;

        public DoctorsSurveyRepository DoctorsSurveyRepository => _DoctorsSurveyRepository;

        public DrugsRepository DrugsRepository => _DrugsRepository;
        
        public Saver Saver
        {
            get => _saver;
        }
    }

}