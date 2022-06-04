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
            _roomManager = new RoomManager(fileSettings.OperatingRoomsFn, fileSettings.OverviewRoomsFn,
                fileSettings.RetiringRoomsFn, fileSettings.StockRoomFn, this);
            _appointmentManager = new AppointmentManager(fileSettings.AppointmentsFn, this);
            _anamnesaManager = new AnamnesaManager(fileSettings.AnamnesaFn, this);
            _requestManager = new RequestManager(fileSettings.RequestedFn, this);
            _saver = saver;
            _timerManager = new TimerManager(fileSettings.TimerFn, this);
            _recipesManager = new RecipesManager(fileSettings.RecipesFn, this);
            _drugManager = new DrugManager(fileSettings.DrugsFn, fileSettings.RejectedDrugsFn);
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
            _recipesManager.LoadData();
            _drugManager.LoadData();
            

        }

        public RecipesManager RecipesManager
        {
            get => _recipesManager;
        }

        public RequestManager RequestManager
        {
            get => _requestManager;
        }

        public DrugManager DrugManager
        {
            get => _drugManager;
        }

        public AnamnesaManager AnamnesaManager
        {
            get => _anamnesaManager;
        }

        public AppointmentManager AppointmentManager
        {
            get => _appointmentManager;

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
    }

}