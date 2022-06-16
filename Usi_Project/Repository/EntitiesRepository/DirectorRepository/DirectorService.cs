using Newtonsoft.Json;
using System;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;


namespace Usi_Project.Repository.EntitiesRepository.DirectorsRepository
{
    public class DirectorService
    {
        private static DirectorRepository _directorRepository;
        public DirectorService(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }
        
        public void Menu()
        {
            while (true)
            {
                DirectorMenus.PrintMainMenu();
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewHospitalRooms();
                        break;
                    case "2":
                        CreateHospitalRooms();
                        break;
                    case "3":
                        HospitalBrowser.SearchHospitalEquipments(_directorRepository.RoomRepository);
                        break;
                    case "4":
                        _directorRepository.TimerManager.RefreshEquipments();
                        CheckIfRenovationIsEnded();
                        break;
                    case "5":
                        RoomRenovation.Renovation(_directorRepository.RoomRepository);
                        break;
                    case "6":
                        RoomRenovation.MultipleRoomRenovation(_directorRepository.RoomRepository, _directorRepository.RoomService);
                        break;
                    case "7":
                        _directorRepository.DrugsRepository.GetOptions();
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
                        SurveysViewer.PrintResultsOfHospitalSurvey(_directorRepository.HospitalSurvey);
                        break;
                    case "2":
                        SurveysViewer.PrintResultsOfDoctorsSurvey(_directorRepository.DoctorSurvey, _directorRepository.DoctorsRepository);
                        break;
                    case "3":
                        SurveysViewer.ViewThreeBestDoctors(_directorRepository.DoctorsRepository.Doctors);
                        break;
                    case "4":
                        SurveysViewer.ViewThreeWorseDoctors(_directorRepository.DoctorsRepository.Doctors);
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
        
         private static void CreateHospitalRooms()
        {
            while (true)
            {
                DirectorMenus.PrintCreatingRoomsOptions();
                RoomService roomService = new RoomService(_directorRepository.RoomRepository);
                switch (Console.ReadLine())
                {
                    case "1":
                        roomService.CreateRoom(typeof(OverviewRoom));
                        break;
                    case "2":
                        roomService.CreateRoom(typeof(OperatingRoom));
                        break;
                    case "3":
                        roomService.CreateRoom(typeof(RetiringRoom));
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
         
         public static void CheckIfRenovationIsEnded()
         {
             RoomService roomService = new RoomService(_directorRepository.RoomRepository);
             roomService.CheckIfRenovationIsEnded();
         }

        private static void ViewHospitalRooms()
        {
            DirectorMenus.PrintViewRoomsOptions();
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                   RoomsViewer.ViewOperatingRooms(_directorRepository.RoomRepository, RoomService.FindOperatingRoom(_directorRepository.RoomRepository.OperatingRooms),
                       _directorRepository.TimerManager);
                    break;
                case "2":
                    RoomsViewer.ViewOverviewRooms(_directorRepository.RoomRepository, RoomService.FindOverviewRoom(_directorRepository.RoomRepository.OverviewRooms), 
                        _directorRepository.TimerManager);
                    break;
                case "3":
                    RoomsViewer.ViewRetiringRoom(_directorRepository.RoomRepository, RoomService.FindRetiringRoom(_directorRepository.RoomRepository.RetiringRooms));
                    break;
                case "4":
                    RoomsViewer.ViewStockRoom(_directorRepository.RoomRepository);
                    break;
            }
        }

    }
}