using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Usi_Project.Users;
namespace Usi_Project.Manage
{
    public class DirectorManager
    {
        private string _directorFilename;
        private Director _director;
        private static Factory _factory;
        private RoomManager _roomManager;

        public DirectorManager(string directorFilename, Factory factory)
        {
            _directorFilename = directorFilename;
            _factory = factory;
            _roomManager = new RoomManager();
        }

        public string DirectorFilename
        {
            get => _directorFilename;
            set => _directorFilename = value;
        }

        public Director Director
        {
            get => _director;
            set => _director = value;
        }

        public Factory Factory
        {
            get => _factory;
            set => _factory = value;
        }

        public RoomManager RoomManager
        {
            get => _roomManager;
            set => _roomManager = value;
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
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - View hospital rooms");
                Console.WriteLine("2) - Create hospital rooms");
                Console.WriteLine("3) - Search hospital equipments");
                Console.WriteLine("4) - Refresh changes");
                Console.WriteLine("5) - Scheduling room renovation");
                Console.WriteLine("6) - Scheduling multiple room renovation");
                Console.WriteLine("x) - Exit.");
                Console.WriteLine(">> ");
                
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewHospitalRooms(_factory);
                        break;
                    case "2":
                        CreateHospitalRooms();
                        break;
                    case "3":
                        SearchHospitalEquipments();
                        break;
                    case "4":
                        _factory.TimerManager.RefreshEquipments();
                        CheckIfRenovationIsEnded();
                        break;
                    case "5":
                        RoomRenovation();
                        break;
                    case "6":
                        MultipleRoomRenovation();
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Invalid option entered, try again");
                        Menu();
                        break;
                }

            }
        }

        private static void PrintRoomsMenu()
        {
            
            Console.WriteLine("Choose type of hospital room: ");
            Console.WriteLine("1) Operating room");
            Console.WriteLine("2) Overview room");
            Console.WriteLine("3) Retiring room");
            Console.WriteLine("4) Stock room");
            Console.WriteLine("5) Ok");
            Console.WriteLine(">> ");
        }

        private static void PrintQuantityMenu()
        {
            Console.WriteLine("Choose quantity of equipment: ");
            Console.WriteLine("1) Out of stock");
            Console.WriteLine("2) 0-10");
            Console.WriteLine("3) 10+");
            Console.WriteLine("4) Ok");
            Console.WriteLine(">> ");
        }
        private static void RoomRenovation()
        {
            PrintRoomsMenu();
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1: 
                    OperatingRoom operatingRoom = _factory.RoomManager.FindOperatingRoom();
                    var timeForRenovationRoom = CheckTimeForRenovationRoom(operatingRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
                    break;
                case 2:
                    OverviewRoom overviewRoom = _factory.RoomManager.FindOverviewRoom();
                    timeForRenovationRoom = CheckTimeForRenovationRoom(overviewRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
                    break;
                case 3:
                    RetiringRoom retiringRoom = _factory.RoomManager.FindRetiringRoom();
                    timeForRenovationRoom = CheckTimeForRenovationRoom(retiringRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        retiringRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
                    break;
            }
        }

        private static void MultipleRoomRenovation()
        {
            Console.WriteLine("Choose one of the renovation options below: ");
            Console.WriteLine("1) Dividing the room into two smaller rooms");
            Console.WriteLine("2) Merging two rooms into one larger room");
            Console.WriteLine(">> ");
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    ChooseRoomForDividing();
                    break;
            }
        }

        private static void ChooseRoomForDividing()
        
        {
            Console.WriteLine("Choose type of hospital room: ");
            Console.WriteLine("1) Operating room");
            Console.WriteLine("2) Overview room");
            Console.WriteLine("3) Retiring room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    OperatingRoom room = _factory.RoomManager.FindOperatingRoom();
                    SplitOperatingRoom(room);
                    break;
                case 2:
                    OverviewRoom overviewRoom = _factory.RoomManager.FindOverviewRoom();
                    SplitOverwievRoom(overviewRoom);
                    break;
                case 3:
                    RetiringRoom retiringRoom = _factory.RoomManager.FindRetiringRoom();
                    SplitRetiringRoom(retiringRoom);
                    break;
            }
        }

        private static void SplitOperatingRoom(OperatingRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + room.TimeOfRenovation.Value + "\n");
                return;
            }
            
            var timeForRenovationRoom = CheckTimeForRenovationRoom(room.Id);
            if (timeForRenovationRoom.Item3)
                room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);

            Console.WriteLine("Create first operating room");
            OperatingRoom firstRoom = CreateOperatingRoom();
            Console.WriteLine("Create second operating room");

            OperatingRoom secondRoom = CreateOperatingRoom();
            foreach (var equipment in room.SurgeryEquipments)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + room.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + room.Name  + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.SurgeryEquipments[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.SurgeryEquipments[equipment.Key] = equipment.Value;
                        break;
                }
                
            }
            SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
            firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
            secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);

            _factory.RoomManager.OperatingRooms.Remove(room);
            
        }

        private static void SplitFurnitureThroughRooms(HospitalRoom parentRoom, HospitalRoom firstRoom,
            HospitalRoom secondRoom)
        {
            foreach (var equipment in parentRoom.Furniture)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + firstRoom.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + secondRoom.Name  + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.Furniture[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.Furniture[equipment.Key] = equipment.Value;
                        break;
                }
            }
        }

        private static void SplitOverwievRoom(OverviewRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + room.TimeOfRenovation.Value + "\n");
                return;
            }
            
            var timeForRenovationRoom = CheckTimeForRenovationRoom(room.Id);
            if (timeForRenovationRoom.Item3)
                room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);

            Console.WriteLine("Create first overview room");
            OverviewRoom firstRoom = CreateOverviewRoom();
            Console.WriteLine("Create second operating room");
            OverviewRoom secondRoom = CreateOverviewRoom();
            foreach (var equipment in room.Tools)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + room.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + room.Name  + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.Tools[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.Tools[equipment.Key] = equipment.Value;
                        break;
                }
                
            }
            SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
            firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
            secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
            _factory.RoomManager.OverviewRooms.Remove(room);

        }

        private static void SplitRetiringRoom(RetiringRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + room.TimeOfRenovation.Value + "\n");
                return;
            }
            var timeForRenovationRoom = CheckTimeForRenovationRoom(room.Id);
            if (timeForRenovationRoom.Item3)
                room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);


            RetiringRoom firstRoom = CreateRetiringRoom();
            RetiringRoom secondRoom = CreateRetiringRoom();
            
            firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
            secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1,timeForRenovationRoom.Item2);
            
            SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
            _factory.RoomManager.RetiringRooms.Remove(room);

        }

        private static (DateTime, DateTime, bool) CheckTimeForRenovationRoom(string idRoom)
        {
            Console.WriteLine("Input time for start room renovation");
            DateTime timeStart = _factory.DoctorManager.CreateDate();
            Console.WriteLine("Input end time for room renovation");
            DateTime timeEnd = _factory.DoctorManager.CreateDate();
            bool isFree = _factory.DoctorManager.CheckRoom(timeStart,
                timeEnd, idRoom);
            return (timeStart, timeEnd, isFree);
        }

        private static void SearchHospitalEquipments()
        {
            Console.WriteLine("Enter equipment: >> ");
            string eq = Console.ReadLine();
            List<int> options = new List<int>();
            while (!options.Contains(5))
            {
                PrintRoomsMenu();
                options.Add(Int32.Parse(Console.ReadLine()));
            }
            PrintQuantityMenu();
            int option2 = Int32.Parse(Console.ReadLine());
            Search(eq, options, option2);
        }

        private static void Search(string eq, List<int> options1, int option2)
        {
            foreach (var q in options1)
            {
                if (q == 1)
                    SearchThroughOperatingRooms(eq.ToLower(), option2);
                else if (q == 2)
                    SearchThroughOverviewRooms(eq.ToLower(), option2);
                else if (q == 3)
                    SearchThroughRetiringRooms(eq.ToLower(), option2);
                else if (q == 4)
                    SearchThroughStockRoom(eq.ToLower(), option2);
            }
        }

        private static void SearchThroughOperatingRooms(string eq, int option)
        {
            foreach (OperatingRoom op in _factory.RoomManager.OperatingRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");

                op.PrintSurgeryEquipments(eq, option);
                op.PrintFurniture(eq, option);
            }
        }

        private static void SearchThroughOverviewRooms(string eq, int option)
        {
            foreach (OverviewRoom op in _factory.RoomManager.OverviewRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintMedicalEquipments(eq, option);
                op.PrintFurniture(eq, option);
            }
        }
        
        private static void SearchThroughRetiringRooms(string eq, int option)
        {
            foreach (RetiringRoom op in _factory.RoomManager.RetiringRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintFurniture(eq, option);
            }
        }
        
        private static void SearchThroughStockRoom(string eq, int option)
        {
            _factory.RoomManager.StockRoom.PrintMedicalEquipment(eq, option);
            _factory.RoomManager.StockRoom.PrintSurgeryEquipment(eq, option);
            _factory.RoomManager.StockRoom.PrintFurniture(eq, option);
        }

        private static void CreateHospitalRooms()
        {
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Create Overview room");
                Console.WriteLine("2) - Create Operating room");
                Console.WriteLine("3) - Create Retiring room");
                Console.WriteLine("x) - Back");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        CreateOverviewRoom();
                        break;
                    case "2":
                        CreateOperatingRoom();
                        break;
                    case "3":
                        CreateRetiringRoom();
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
        public static OverviewRoom CreateOverviewRoom()
        {
            while (true)
            {
                bool ind = false;
                Console.WriteLine("Input id of new Overview Room >> ");
                string id = Console.ReadLine();
                foreach (OverviewRoom overviewRoom in _factory.RoomManager.OverviewRooms)
                {
                    if (overviewRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                        ind = true;
                        break;
                    }
                }
                if (ind) continue;
                Console.WriteLine("Input name of new Overview room >> ");
                string name = Console.ReadLine();
                OverviewRoom newRoom = new OverviewRoom(id, name);
                _factory.RoomManager.OverviewRooms.Add(newRoom);
                return newRoom;
            }   
        }

        public static OperatingRoom CreateOperatingRoom()
        {  while (true)
            {
                Console.WriteLine("Input id of new Operating Room >> ");
                var id = Console.ReadLine();
                foreach (var operatingRoom in _factory.RoomManager.OperatingRooms)
                {
                    if (operatingRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }
                Console.WriteLine("Input name of new Operating room >> ");
                var roomName = Console.ReadLine();
                OperatingRoom ovpRoom = new OperatingRoom(id, roomName);
                _factory.RoomManager.OperatingRooms.Add(ovpRoom);
                return ovpRoom;
            }
        }

        public static RetiringRoom CreateRetiringRoom()
        {
            while (true)
            {
                Console.WriteLine("Input id of new Retiring Room >> ");
                string id = Console.ReadLine();
                foreach (RetiringRoom retiringRoom in _factory.RoomManager.RetiringRooms)
                {
                    if (retiringRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }
                Console.WriteLine("Input name of new Retiring room >> ");
                string roomName = Console.ReadLine();
                RetiringRoom newRetiringRoom = new RetiringRoom(id, roomName);
                _factory.RoomManager.RetiringRooms.Add(newRetiringRoom);
                return newRetiringRoom;
            }   
        }
        public static void ViewHospitalRooms(Factory factory)
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1. View Operating rooms");
            Console.WriteLine("2. View Overview rooms");
            Console.WriteLine("3. View Stock room");
            Console.Write(">> ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    factory.RoomManager.ViewOperatingRooms(factory.RoomManager.FindOperatingRoom());
                    break;
                case "2":
                    factory.RoomManager.ViewOverviewRooms(factory.RoomManager.FindOverviewRoom());
                    break;
                case "3":
                    break;
            }
        }

        public static  void CheckIfRenovationIsEnded()
        {
            while (true)
            {
                foreach (var operatingRoom in _factory.RoomManager.OperatingRooms.ToList())
                {
                    if (DateTime.Now >= operatingRoom.TimeOfRenovation.Value)
                        operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                }
                foreach (var overviewRoom in _factory.RoomManager.OverviewRooms.ToList())
                {
                    if (DateTime.Now >= overviewRoom.TimeOfRenovation.Value)
                        overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
                }
                foreach (var retiringRoom in _factory.RoomManager.RetiringRooms.ToList())
                {
                    if (DateTime.Now >= retiringRoom.TimeOfRenovation.Value)
                        retiringRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>();
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