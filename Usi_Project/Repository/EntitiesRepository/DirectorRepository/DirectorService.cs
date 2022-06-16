using Newtonsoft.Json;
using System;
using Usi_Project.Repository.DrugRepository;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;


namespace Usi_Project.Repository.EntitiesRepository.DirectorsRepository
{
    public class DirectorService
    {
        private static DirectorRepository _directorRepository;
        private static DrugService _drugService;
        public DirectorService(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
            _drugService = new DrugService(_directorRepository.DrugsRepository);
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
                        RoomRenovation.MultipleRoomRenovation(_directorRepository.RoomRepository, _directorRepository.RoomFinder);
                        break;
                    case "7":
                        _drugService.GetOptions();
                        break;
                    case "8": 
                        ViewSurveysResults();
                        break;
                    case "x":
                        return;
                    default:
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
                    default:
                        return;

                    
                }
                
            }
 
            
        }
        
         private static void CreateHospitalRooms()
        {
            while (true)
            {
                DirectorMenus.PrintCreatingRoomsOptions();
                RoomFinder roomFinder = new RoomFinder(_directorRepository.RoomRepository);
                switch (Console.ReadLine())
                {
                    case "1":
                        roomFinder.CreateRoom(typeof(OverviewRoom));
                        break;
                    case "2":
                        roomFinder.CreateRoom(typeof(OperatingRoom));
                        break;
                    case "3":
                        roomFinder.CreateRoom(typeof(RetiringRoom));
                        break;
                    case "x":
                        return;
                    default:
                        return;
                }
            }

        }
         
         public static void CheckIfRenovationIsEnded()
         {
             RoomFinder roomFinder = new RoomFinder(_directorRepository.RoomRepository);
             roomFinder.CheckIfRenovationIsEnded();
         }

        private static void ViewHospitalRooms()
        {
            DirectorMenus.PrintViewRoomsOptions();
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                   RoomsViewer.ViewOperatingRooms(_directorRepository.RoomRepository, RoomFinder.FindOperatingRoom(_directorRepository.RoomRepository.OperatingRooms),
                       _directorRepository.TimerManager);
                    break;
                case "2":
                    RoomsViewer.ViewOverviewRooms(_directorRepository.RoomRepository, RoomFinder.FindOverviewRoom(_directorRepository.RoomRepository.OverviewRooms), 
                        _directorRepository.TimerManager);
                    break;
                case "3":
                    RoomsViewer.ViewRetiringRoom(_directorRepository.RoomRepository, RoomFinder.FindRetiringRoom(_directorRepository.RoomRepository.RetiringRooms));
                    break;
                case "4":
                    RoomsViewer.ViewStockRoom(_directorRepository.RoomRepository);
                    break;
            }
        }

    }
}