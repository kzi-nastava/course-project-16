using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class DirectorManager
    {
        private readonly string _directorFilename;
        private Director _director;
        private static Factory _factory;

        public DirectorManager(string directorFilename, Factory factory)
        {
            _directorFilename = directorFilename;
            _factory = factory;

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
                        HospitalBrowser.SearchHospitalEquipments(_factory);
                        break;
                    case "4":
                        _factory.TimerManager.RefreshEquipments();
                        _factory.DirectorManager.CheckIfRenovationIsEnded();
                        break;
                    case "5":
                        RoomRenovation.Renovation(_factory);
                        break;
                    case "6":
                        RoomRenovation.MultipleRoomRenovation(_factory);
                        break;
                    case "7":
                        _factory.DrugManager.PrintMenu();
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
                    case "2":
                        SurveysViewer.PrintResultsOfDoctorsSurvey(_factory);
                        break;
                    case "3":
                        SurveysViewer.ViewThreeBestDoctors(_factory);
                        break;
                    case "4":
                        SurveysViewer.ViewThreeWorseDoctors(_factory);
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
                        RoomsMaker.CreateOverviewRoom(_factory);
                        break;
                    case "2":
                        RoomsMaker.CreateOperatingRoom(_factory);
                        break;
                    case "3":
                        RoomsMaker.CreateRetiringRoom(_factory);
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
                   RoomsViewer.ViewOperatingRooms(_factory, RoomsBrowser.FindOperatingRoom(_factory.RoomManager.OperatingRooms));
                    break;
                case "2":
                    RoomsViewer.ViewOverviewRooms(_factory, RoomsBrowser.FindOverviewRoom(_factory.RoomManager.OverviewRooms));
                    break;
                case "3":
                    RoomsViewer.ViewRetiringRoom(_factory, RoomsBrowser.FindRetiringRoom(_factory.RoomManager.RetiringRooms));
                    break;
                case "4":
                    RoomsViewer.ViewStockRoom(_factory);
                    break;
            }
        }

        public void CheckIfRenovationIsEnded()
        {

            foreach (var operatingRoom in _factory.RoomManager.OperatingRooms.ToList())
            {
                if (DateTime.Now >= operatingRoom.TimeOfRenovation.Value)
                {
                    operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                    operatingRoom.ForRemove = false;
                }
            }

            foreach (var overviewRoom in _factory.RoomManager.OverviewRooms.ToList())
            {
                if (DateTime.Now >= overviewRoom.TimeOfRenovation.Value)
                {
                    overviewRoom.ForRemove = false;
                    overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                }
            }

            foreach (var retiringRoom in _factory.RoomManager.RetiringRooms.ToList())
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