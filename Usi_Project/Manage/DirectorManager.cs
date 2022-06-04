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
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - View hospital rooms");
                Console.WriteLine("2) - Create hospital rooms");
                Console.WriteLine("3) - Search hospital equipments");
                Console.WriteLine("4) - Refresh changes");
                Console.WriteLine("5) - Scheduling room renovation");
                Console.WriteLine("6) - Scheduling multiple room renovation");
                Console.WriteLine("7) - Drug and ingredient management");
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
                        _factory.DirectorManager.CheckIfRenovationIsEnded();
                        break;
                    case "5":
                        RoomRenovation();
                        break;
                    case "6":
                        MultipleRoomRenovation();
                        break;
                    case "7":
                        _factory.DrugManager.PrintMenu();
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

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_directorFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _director);
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
                case 2:
                    ChooseRoomsForMerging();
                    break;
            }
        }

        private static void PrintMenuForMergingRooms()
        {
            Console.WriteLine("Choose type of hospital room for merging: ");
            Console.WriteLine("1) Merge rooms of same type");
            Console.WriteLine("2) Merge operating room with retiring room");
            Console.WriteLine("3) Merge operating room with overview room");
            Console.WriteLine("4) Merge overview room with retiring room");
            Console.WriteLine(">> ");
        }

        private static void ChooseRoomsForMerging()
        {
            PrintMenuForMergingRooms();
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergeRoomsOfSameType();
                    break;
                case 2:
                    Console.WriteLine("\nChoose operating room\n");
                    OperatingRoom operatingRoom = _factory.RoomManager.FindOperatingRoom();
                    Console.WriteLine("\nChoose  retiring room\n");
                    RetiringRoom retiringRoom = _factory.RoomManager.FindRetiringRoom();
                    if (IsBeingRenovated(operatingRoom) || IsBeingRenovated(retiringRoom))
                        return;
                    MergeOperatingAndRetiringRoom(operatingRoom, retiringRoom);
                    break;
                case 4:
                    Console.WriteLine("\nChoose  retiring room\n");
                    retiringRoom = _factory.RoomManager.FindRetiringRoom();
                    Console.WriteLine("\nChoose overview room\n");
                    OverviewRoom overviewRoom = _factory.RoomManager.FindOverviewRoom();
                    MergeRetiringRoomAndOverviewRoom(retiringRoom, overviewRoom);
                    break;
                case 3:
                    Console.WriteLine("\nChoose  operating room\n");
                    operatingRoom = _factory.RoomManager.FindOperatingRoom();
                    Console.WriteLine("\nChoose overview room\n");
                    overviewRoom = _factory.RoomManager.FindOverviewRoom();
                    MergeOperatingAndOverviewRoom(operatingRoom, overviewRoom);
                    break;
                    
                    
            }
        }

        private static void MergeRoomsOfSameType()
        {
            Console.WriteLine("1) Merge into new Operating room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine("3) Merge into new Retiring room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergeOperatingRooms();
                    break;
                case 2:
                    MergeOverviewRooms();
                    break;
                case 3:
                    MergeRetiringRooms();
                    break;
            }
        }

        private static bool IsBeingRenovated(HospitalRoom hospitalRoom)
        {
            if (!hospitalRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + hospitalRoom.TimeOfRenovation.Value + "\n");
                return true;
            }

            return false;
        }

        private static void SetTimeForRenovation(HospitalRoom hospitalRoom, DateTime start, DateTime end)
        {
            bool isFree = _factory.DoctorManager.CheckRoom(start, end, hospitalRoom.Id);
            if (isFree)
                hospitalRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    start, end);
        }

        private static (DateTime, DateTime) GetStartAndEndTimeForRenovation()
        {
            Console.WriteLine("Input time for start room renovation");
            DateTime timeStart = _factory.DoctorManager.CreateDate();
            Console.WriteLine("Input end time for room renovation");
            DateTime timeEnd = _factory.DoctorManager.CreateDate();
            return (timeStart, timeEnd);
        }

        private static void MergeOperatingRooms()
        {
            
            Console.WriteLine("\nChoose first operating room\n");
            OperatingRoom first = _factory.RoomManager.FindOperatingRoom();
            Console.WriteLine("\nChoose second operating room\n");
            OperatingRoom second = _factory.RoomManager.FindOperatingRoom();
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }
            if (IsBeingRenovated(first) || IsBeingRenovated(second))
                return;
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new operating room\n");
            OperatingRoom newOperatingRoom = CreateOperatingRoom();
            newOperatingRoom.SurgeryEquipments =  MergeSurgeryEquipments(first, second);
            newOperatingRoom.Furniture = MergeFurniture(first, second);
            SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }

        private static Dictionary<SurgeryTool, int> MergeSurgeryEquipments(OperatingRoom first, OperatingRoom second)
        {
            Dictionary<SurgeryTool, int> ret = new Dictionary<SurgeryTool, int>();
            foreach (var tool in first.SurgeryEquipments)
            {
                ret[tool.Key] = tool.Value;
            }
            foreach (var tool in second.SurgeryEquipments)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;
        }

        private static void MergeRetiringRooms()
        {
            Console.WriteLine("\nChoose first overview room\n");
            RetiringRoom first = _factory.RoomManager.FindRetiringRoom();
            Console.WriteLine("\nChoose second overview room\n");
            RetiringRoom second = _factory.RoomManager.FindRetiringRoom();
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }
            if (IsBeingRenovated(first) || IsBeingRenovated(second))
                return;
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new overview room\n");
            RetiringRoom newRetiringRoom = CreateRetiringRoom();
            newRetiringRoom.Furniture = MergeFurniture(first, second);
            SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;
      
        }

        private static void MergeOverviewRooms()
        {
            Console.WriteLine("\nChoose first overview room\n");
            OverviewRoom first = _factory.RoomManager.FindOverviewRoom();
            Console.WriteLine("\nChoose second overview room\n");
            OverviewRoom second = _factory.RoomManager.FindOverviewRoom();
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }
            if (IsBeingRenovated(first) || IsBeingRenovated(second))
                return;
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new overview room\n");
            OverviewRoom newOverviewRoom = CreateOverviewRoom();
            newOverviewRoom.Tools =  MergeMedicalEquipments(first, second);
            newOverviewRoom.Furniture = MergeFurniture(first, second);
            SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;
            
        }

        private static Dictionary<MedicalTool, int> MergeMedicalEquipments(OverviewRoom first, OverviewRoom second)
        {
            Dictionary<MedicalTool, int> ret = new Dictionary<MedicalTool, int>();
            foreach (var tool in first.Tools)
            {
                ret[tool.Key] = tool.Value;
            }
            foreach (var tool in second.Tools)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;
            
        }
        private static void MergeOperatingAndOverviewRoom(OperatingRoom operatingRoom, OverviewRoom overviewRoom)
        {
            
            Console.WriteLine("1) Merge into new Operating room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(operatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(overviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            switch (choiseRoom)
            {
                case 1:
                    OperatingRoom newOperatingRoom = CreateOperatingRoom();
                    newOperatingRoom.SurgeryEquipments = operatingRoom.SurgeryEquipments;
                    newOperatingRoom.Furniture = MergeFurniture(operatingRoom, overviewRoom);
                    SendMedicalEquipmentToStockRoom(overviewRoom);
                    Console.WriteLine("Medical Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
                case 2:
                    OverviewRoom newOverviewRoom = CreateOverviewRoom();
                    newOverviewRoom.Tools = overviewRoom.Tools;
                    newOverviewRoom.Furniture = MergeFurniture(operatingRoom, overviewRoom);
                    SendSurgeryEquipmentToStockRoom(operatingRoom);
                    Console.WriteLine("Surgery Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }
            overviewRoom.ForRemove = true;
            operatingRoom.ForRemove = true;
            
        }

        private static void SendSurgeryEquipmentToStockRoom(OperatingRoom operatingRoom)
        {
            foreach (var tool in operatingRoom.SurgeryEquipments)
            {
                if (!_factory.RoomManager.StockRoom.SurgeryEquipment.ContainsKey(tool.Key))
                {
                    _factory.RoomManager.StockRoom.SurgeryEquipment[tool.Key] = tool.Value;
                }
                _factory.RoomManager.StockRoom.SurgeryEquipment[tool.Key] += tool.Value;
            }
        }

        private static void SendMedicalEquipmentToStockRoom(OverviewRoom overviewRoom)
        {
            foreach (var tool in overviewRoom.Tools)
            {
                if (!_factory.RoomManager.StockRoom.MedicalEquipment.ContainsKey(tool.Key))
                {
                    _factory.RoomManager.StockRoom.MedicalEquipment[tool.Key] = tool.Value;
                }
                _factory.RoomManager.StockRoom.MedicalEquipment[tool.Key] += tool.Value;
            }
        }

        private static  Dictionary<Furniture,int> MergeFurniture(HospitalRoom hospitalRoom1, HospitalRoom hospitalRoom2)
        {
            Dictionary<Furniture, int> ret = new Dictionary<Furniture, int>();
            foreach (var tool in hospitalRoom1.Furniture)
            {
                ret[tool.Key] = tool.Value;
            }
            foreach (var tool in hospitalRoom2.Furniture)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;
            
        }

        private static void  MergeRetiringRoomAndOverviewRoom(RetiringRoom retiringRoom, OverviewRoom overviewRoom)
        {

            Console.WriteLine("1) Merge into new Retiring room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(retiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(overviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
          
            switch (choiseRoom)
            {
                case 1:
                    RetiringRoom newRetiringRoom = CreateRetiringRoom();
                    newRetiringRoom.Furniture = MergeFurniture(retiringRoom, overviewRoom);
                    SendMedicalEquipmentToStockRoom(overviewRoom);
                    Console.WriteLine("Medical Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);

                    break;
                case 2:
                    OverviewRoom newOverviewRoom = CreateOverviewRoom();
                    newOverviewRoom.Tools = overviewRoom.Tools;
                    newOverviewRoom.Furniture = MergeFurniture(retiringRoom, overviewRoom); ;
                    SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }
            overviewRoom.ForRemove = true;
            retiringRoom.ForRemove = true;

        }

        private static void MergeOperatingAndRetiringRoom(OperatingRoom operatingRoom, RetiringRoom retiringRoom)
        {
            
            Console.WriteLine("1) Merge into new Retiring room");
            Console.WriteLine("2) Merge into new Operating room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(retiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(operatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            switch (choiseRoom)
            {
                case 1:
                    RetiringRoom newRetiringRoom = CreateRetiringRoom();
                    newRetiringRoom.Furniture = MergeFurniture(retiringRoom, operatingRoom);
                    SendSurgeryEquipmentToStockRoom(operatingRoom);
                    Console.WriteLine("Surgery Equipments from operating room send to Stock Room.");
                    SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
                case 2:
                    OperatingRoom newOperatingRoom = CreateOperatingRoom();
                    newOperatingRoom.Furniture = MergeFurniture(retiringRoom, operatingRoom);
                    newOperatingRoom.SurgeryEquipments = operatingRoom.SurgeryEquipments;
                    SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }
            retiringRoom.ForRemove = true;
            operatingRoom.ForRemove = true;

            
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
            Console.WriteLine("3. View Retiring rooms");
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
                    factory.RoomManager.ViewRetiringRoom(factory.RoomManager.FindRetiringRoom());
                    break;
                case "4":
                    break;
            }
        }

        public  void CheckIfRenovationIsEnded()
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