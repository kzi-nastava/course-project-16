using Usi_Project.Settings;
using Usi_Project.Manage;

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

        public Factory(FileSettings fileSettings)
        {
            _directorManager = new DirectorManager(fileSettings.DirectorFilename, this);
            _patientManager = new PatientManager(fileSettings.PatientFilename, this);
            _secretaryManager = new SecretaryManager(fileSettings.SecretaryFilename, this);
            _doctorManager = new DoctorManager(fileSettings.DoctorFilename, this);
            _roomManager = new RoomManager(fileSettings.OperatingRoomsFn, fileSettings.OverviewRoomsFn,
                fileSettings.RetiringRoomsFn, this);
            _appointmentManager = new AppointmentManager(fileSettings.AppointmentsFn, this);
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