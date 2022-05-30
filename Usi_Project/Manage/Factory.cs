using System.IO;
using Newtonsoft.Json;
using Usi_Project.Settings;
using Usi_Project.Manage;
using Newtonsoft.Json;
using Usi_Project.DataSaver;

namespace Usi_Project.Manage
{
    public class Factory
    {
        private DirectorManager _directorManager;
        private PatientManager _patientManager;
        private SecretaryManager _secretaryManager;
        private DoctorManager _doctorManager;
        private RoomManager _roomManager;
        private AppointmentManager _appointmentManager;
        private AnamnesaManager _anamnesaManager;
        private RequestManager _requestManager;
        private TimerManager _timerManager;
        private Saver _saver;
        private RecipesManager _recipesManager;
        private DrugManager _drugManager;
        public Factory()
        {
        }

        public Saver Saver
        {
            get => _saver;
            set => _saver = value;
        }
        
        public Factory(FileSettings fileSettings, Saver saver)

        {
            _directorManager = new DirectorManager(fileSettings.DirectorFilename, this);
            _patientManager = new PatientManager(fileSettings.PatientFilename, this);
            _secretaryManager = new SecretaryManager(fileSettings.SecretaryFilename, this);
            _doctorManager = new DoctorManager(fileSettings.DoctorFilename, this);
            _roomManager = new RoomManager(fileSettings.OperatingRoomsFn, fileSettings.OverviewRoomsFn,
                fileSettings.RetiringRoomsFn, fileSettings.StockRoomFn, this);
            _appointmentManager = new AppointmentManager(fileSettings.AppointmentsFn, this);
            _anamnesaManager = new AnamnesaManager(fileSettings.AnamnesaFn, this);
            _requestManager = new RequestManager(fileSettings.RequestedFn, this);
            _saver = saver;
            _timerManager = new TimerManager(fileSettings.TimerFn, this);
            _recipesManager = new RecipesManager(fileSettings.RecipesFn, this);
            _drugManager = new DrugManager(fileSettings.DrugsFn, fileSettings.RejectedDrugsFn, this);
        }

        public RecipesManager RecipesManager
        {
            get => _recipesManager;
            set => _recipesManager = value;
        }

        public RequestManager RequestManager
        {
            get => _requestManager;
            set => _requestManager = value;
        }

        public DrugManager DrugManager
        {
            get => _drugManager;
            set => _drugManager = value;
        }

        public AnamnesaManager AnamnesaManager
        {
            get => _anamnesaManager;
            set => _anamnesaManager = value;
           
        
        }

        public AppointmentManager AppointmentManager
        {
            get => _appointmentManager;
            set => _appointmentManager = value;
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
            _drugManager.LoadData();
            

        }
        public TimerManager TimerManager
        {
            get => _timerManager;
            set => _timerManager = value;

        }

        public RoomManager RoomManager
        {
            get => _roomManager;
            set => _roomManager = value;
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