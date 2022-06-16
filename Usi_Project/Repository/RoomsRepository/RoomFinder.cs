using System;
using System.Collections.Generic;
using System.Linq;
using Usi_Project.Repository.EntitiesRepository.DirectorsRepository;

namespace Usi_Project.Repository
{
    public class RoomFinder
    {
        private static RoomRepository _roomRepository;

        public RoomFinder(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void CheckIfRenovationIsEnded()
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

        private bool IfRoomExistsWithId(string id)
        {
            foreach (OverviewRoom overviewRoom in _roomRepository.OverviewRooms)
            {
                if (overviewRoom.Id == id)
                    return true;
            }

            return false;
        }

        public HospitalRoom CreateRoom(Type type)
        {
            string id;
            while (true)
            {
                id = InputRoomsInfo.GetInfoForCreatingNewRoom();
                if (IfRoomExistsWithId(id))
                {
                    InputRoomsInfo.SetWarningThatIdExists(id);
                    continue;
                }
                break;
            }
            var name = InputRoomsInfo.GetNameOfNewRoom();
            return _roomRepository.CreateRoom(type, id, name);
        }



      public static OverviewRoom FindOverviewRoom(List<OverviewRoom> _overviewRooms)
        {
            int i = 1;
            Dictionary<int, OverviewRoom> dictionary = new Dictionary<int, OverviewRoom>();
            foreach (var opp in _overviewRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see overview room: >> ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }

        public static OperatingRoom FindOperatingRoom(List<OperatingRoom> _operatingRooms)
        {
            int i = 1;
            Dictionary<int, OperatingRoom> dictionary = new Dictionary<int, OperatingRoom>();
            foreach (var opp in _operatingRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see operating room: >>  ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }

        public static RetiringRoom FindRetiringRoom(List<RetiringRoom> _retiringRooms)
        {
            int i = 1;
            Dictionary<int, RetiringRoom> dictionary = new Dictionary<int, RetiringRoom>();
            foreach (var opp in _retiringRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see retiring room: >>  ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }
        
        public static void ChangeRetiringRoom(RetiringRoom retiring)
        {
            if (!retiring.IsDateTimeOfRenovationDefault())
            {
                DirectorService.CheckIfRenovationIsEnded();
                if (!retiring.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + retiring.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.WriteLine("Enter new name:  ");
                        string newName = Console.ReadLine();
                        retiring.Name = newName;
                        break;
                    }
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public static void ChangeOvRoom(RoomRepository _manager, OverviewRoom overview, TimerManager timerManager)
        {
            if (!overview.IsDateTimeOfRenovationDefault())
            {
                DirectorService.CheckIfRenovationIsEnded();
                if (!overview.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + overview.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.WriteLine("3) Change Medical Equipments");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.WriteLine("Enter new name: >> ");
                        string newName = Console.ReadLine();
                        overview.Name = newName;
                        break;
                    }
                    case "2":
                        FurnitureChanger.ChangeFurnitureInOvRoom(_manager, overview, timerManager);
                        break;
                        
                    case "3":
                        EquipmentChanger.ChangeMedicalTools(_manager, overview, timerManager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
         public static void ChangeOpRoom(RoomRepository repository, OperatingRoom operatingRoom, TimerManager timerManager)
        {
            if (!operatingRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + operatingRoom.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {

                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.WriteLine("3) Change Surgery Equipments");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.Write("Enter new name: >> ");
                        string newName = Console.ReadLine();
                        operatingRoom.Name = newName;
                        break;
                    }
                    case "2":
                        FurnitureChanger.ChangeFurnitureInOpRoom(repository, operatingRoom, timerManager);
                        break;
                    case "3":
                        EquipmentChanger.ChangeSurgeryTools(repository, operatingRoom, timerManager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
    
    }
}