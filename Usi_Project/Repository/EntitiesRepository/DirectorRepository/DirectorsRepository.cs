using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Usi_Project.Manage;
using Usi_Project.Repository;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.Users;

namespace Usi_Project.roomRepository.EntitiesRepository.DirectorRepository
{
    public class DirectorsRepository
    {
        private readonly string _directorFilename;
        private Director _director;
        private static RoomRepository _roomRepository;
        private static HospitalSurveyManager _hospitalSurvey;
        private static DoctorsSurveyRepository _doctorSurvey;
        private static TimerManager _timerManager;
        private static DrugsRepository _DrugsRepository;
        private static DoctorsRepository _DoctorsRepository;

        public DirectorsRepository(string directorFilename, RoomRepository roomRepository,
                                HospitalSurveyManager hospitalSurvey, DoctorsSurveyRepository doctorSurvey,
                                TimerManager timerManager, DrugsRepository DrugsRepository, DoctorsRepository DoctorsRepository)
                                
        {
            _directorFilename = directorFilename;
            _roomRepository = roomRepository;
            _hospitalSurvey = hospitalSurvey;
            _doctorSurvey = doctorSurvey;
            _DoctorsRepository = DoctorsRepository;
            _timerManager = timerManager;
            _DrugsRepository = DrugsRepository;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _director = JsonConvert.DeserializeObject<Director>(File.ReadAllText(_directorFilename), json);
        }

        public static void Menu()
        {
            while (true)
            {
                DirectorMenus.PrintMainMenu();
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewHospitalRooms();
                        break;
                    case "2":
                        CreateHospitalRooms();
                        break;
                    case "3":
                        HospitalBrowser.SearchHospitalEquipments(_roomRepository);
                        break;
                    case "4":
                        _timerManager.RefreshEquipments();
                        CheckIfRenovationIsEnded();
                        break;
                    case "5":
                        RoomRenovation.Renovation(_roomRepository);
                        break;
                    case "6":
                        RoomRenovation.MultipleRoomRenovation(_roomRepository);
                        break;
                    case "7":
                        _DrugsRepository.GetOptions();
                        break;
                    case "8": 
                        ViewSurveysResults();
                        break;
                    case "x":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option entered, try again");
                        Menu();
                        break;
                }

            }
        }

        private static void  ViewSurveysResults()
        {
            while (true)
            {
                DirectorMenus.PrintSurveysMenu();
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        SurveysViewer.PrintResultsOfHospitalSurvey(_hospitalSurvey);
                        break;
                    case "2":
                        SurveysViewer.PrintResultsOfDoctorsSurvey(_doctorSurvey, _DoctorsRepository);
                        break;
                    case "3":
                        SurveysViewer.ViewThreeBestDoctors(_DoctorsRepository.Doctors);
                        break;
                    case "4":
                        SurveysViewer.ViewThreeWorseDoctors(_DoctorsRepository.Doctors);
                        break;
                    case "x":
                        return;
                    default:
                    {
                        Console.WriteLine("Wrong input.");
                        continue;
                    }
                }
                
            }
 
            
        }

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_directorFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _director);
            }
        }

        private static void CreateHospitalRooms()
        {
            while (true)
            {
                DirectorMenus.PrintCreatingRoomsOptions();
                switch (Console.ReadLine())
                {
                    case "1":
                        RoomsMaker.CreateOverviewRoom(_roomRepository);
                        break;
                    case "2":
                        RoomsMaker.CreateOperatingRoom(_roomRepository);
                        break;
                    case "3":
                        RoomsMaker.CreateRetiringRoom(_roomRepository);
                        break;
                    case "x":
                        return;
                    default:
                    {
                        Console.WriteLine("Wrong input.");
                        continue;
                    }
                }
            }

        }

        private static void ViewHospitalRooms()
        {
            DirectorMenus.PrintViewRoomsOptions();
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                   RoomsViewer.ViewOperatingRooms(_roomRepository, RoomsBrowser.FindOperatingRoom(_roomRepository.OperatingRooms), _timerManager);
                    break;
                case "2":
                    RoomsViewer.ViewOverviewRooms(_roomRepository, RoomsBrowser.FindOverviewRoom(_roomRepository.OverviewRooms), _timerManager);
                    break;
                case "3":
                    RoomsViewer.ViewRetiringRoom(_roomRepository, RoomsBrowser.FindRetiringRoom(_roomRepository.RetiringRooms));
                    break;
                case "4":
                    RoomsViewer.ViewStockRoom(_roomRepository);
                    break;
            }
        }

        public static void CheckIfRenovationIsEnded()
        {

            foreach (var operatingRoom in _roomRepository.OperatingRooms.ToList())
            {
                if (DateTime.Now >= operatingRoom.TimeOfRenovation.Value)
                {
                    operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                    operatingRoom.ForRemove = false;
                }
            }

            foreach (var overviewRoom in _roomRepository.OverviewRooms.ToList())
            {
                if (DateTime.Now >= overviewRoom.TimeOfRenovation.Value)
                {
                    overviewRoom.ForRemove = false;
                    overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                }
            }

            foreach (var retiringRoom in _roomRepository.RetiringRooms.ToList())
            {
                if (DateTime.Now >= retiringRoom.TimeOfRenovation.Value)
                {
                    retiringRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                    retiringRoom.ForRemove = false;
                }
            }
        }

        public Director CheckPersonalInfo(string email, string password) =>
            email == _director.email && password == _director.password ? _director : null;

        public bool CheckEmail(string email)
        {
            return email == _director.email;
        }
    }
}