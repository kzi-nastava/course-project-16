using System.IO;
using Newtonsoft.Json;
using Usi_Project.Settings;
using Newtonsoft.Json;
using Usi_Project.DataSaver;
using Usi_Project.Manage;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;
using Usi_Project.Repository.RoomRepository;

namespace Usi_Project.Repository
{
    public class Factory
    {

        private DynamicRequestManager _dynamicRequestManager;
        private readonly DirectorManager _directorManager;
        private readonly PatientManager _patientManager;
        private readonly SecretaryManager _secretaryManager;
        private readonly DoctorManager _doctorManager;
        private readonly RoomManager _roomManager;
        private readonly AppointmentManager _appointmentManager;
        private readonly AnamnesaManager _anamnesaManager;
        private readonly RequestManager _requestManager;
        private readonly TimerManager _timerManager;
        private readonly Saver _saver;
        private readonly RecipesManager _recipesManager;
        private readonly DrugManager _drugManager;
        public Factory()
        {
        }

        public Saver Saver
        {
            get => _saver;
        }
        
        public Factory(FileSettings fileSettings, Saver saver)

        {
            _directorManager = new DirectorManager(fileSettings.DirectorFilename, this);
            _patientManager = new PatientManager(fileSettings.PatientFilename, this);
            _secretaryManager = new SecretaryManager(fileSettings.SecretaryFilename, this);
            _doctorManager = new DoctorManager(fileSettings.DoctorFilename, this);
            _roomManager = new RoomManager(fileSettings.OperatingRoomsFilename, fileSettings.OverviewRoomsFilename,
                fileSettings.RetiringRoomsFilename, fileSettings.StockRoomFilename, this);
            _appointmentManager = new AppointmentManager(fileSettings.AppointmentsFilename, this);
            _anamnesaManager = new AnamnesaManager(fileSettings.AnamnesaFilename, this);
            _requestManager = new RequestManager(fileSettings.RequestedFilename, this);
            _saver = saver;
            _timerManager = new TimerManager(fileSettings.TimerFilename, this);
            _recipesManager = new RecipesManager(fileSettings.RecipesFilename, this);
            _dynamicRequestManager = new DynamicRequestManager(fileSettings.DynamicReqFilename, this);
            _drugManager = new DrugManager(fileSettings.DrugsFilename, fileSettings.RejectedDrugsFilename);
        }



        public void LoadData()
        {

            _directorManager.LoadData();
            _patientManager.LoadData();
            _secretaryManager.LoadData();
            _doctorManager.LoadData();
            _roomManager.LoadData();
            _appointmentManager.LoadData();
            _anamnesaManager.LoadData();
            _requestManager.LoadData();
            _timerManager.LoadData();
            _dynamicRequestManager.LoadData();
            _drugManager.LoadData();
            _recipesManager.LoadData();
        }
        
        public TimerManager TimerManager
        {
            get => _timerManager;

        }

        public RoomManager RoomManager
        {
            get => _roomManager;
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

        public DrugManager DrugManager => _drugManager;
    }

}