using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Usi_Project.Manage
{
    public class RoomManager
    {
        private readonly string _operatingRoomsFn;
        private readonly string _overviewRoomsFn;
        private readonly string _retiringRoomsFn;
        private readonly string _stockRoomFn;
        private static Factory _manager;
        private StockRoom _stockRoom;
        private List<OverviewRoom> _overviewRooms;
        private List<OperatingRoom> _operatingRooms;
        private List<RetiringRoom> _retiringRooms;
        
        public RoomManager()
        {
            _operatingRooms = new List<OperatingRoom>();
            _overviewRooms = new List<OverviewRoom>();
            _retiringRooms = new List<RetiringRoom>();
        }
        public RoomManager(string operatingRoomsFn, string overviewRoomsFn, string retiringRoomsFn, string stockRoomFn,
            Factory factory)
        {
            _operatingRoomsFn = operatingRoomsFn;
            _overviewRoomsFn = overviewRoomsFn;
            _retiringRoomsFn = retiringRoomsFn;
            _stockRoomFn = stockRoomFn;
            _operatingRooms = new List<OperatingRoom>();
            _overviewRooms = new List<OverviewRoom>();
            _retiringRooms = new List<RetiringRoom>();
            _manager = factory;
            
        }

        public StockRoom StockRoom
        {
            get => _stockRoom;
            set => _stockRoom = value;
        }


        public RoomManager(List<OverviewRoom> overviewRooms, List<OperatingRoom> operatingRooms, List<RetiringRoom> retiringRooms)
        {
            _overviewRooms = overviewRooms;
            _operatingRooms = operatingRooms;
            _retiringRooms = retiringRooms;
        }
        
        public void LoadData()
        {
            
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            
            _overviewRooms = JsonConvert.DeserializeObject<List<OverviewRoom>>(File.ReadAllText(_overviewRoomsFn), json);
            _operatingRooms = JsonConvert.DeserializeObject<List<OperatingRoom>>(File.ReadAllText(_operatingRoomsFn), json);
            _retiringRooms = JsonConvert.DeserializeObject<List<RetiringRoom>>(File.ReadAllText(_retiringRoomsFn), json);
            _stockRoom = JsonConvert.DeserializeObject<StockRoom>(File.ReadAllText(_stockRoomFn), json);
        }

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_overviewRoomsFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _overviewRooms);
            }
            
            using (StreamWriter file = File.CreateText(_operatingRoomsFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _operatingRooms);
            }
            
            using (StreamWriter file = File.CreateText(_retiringRoomsFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _retiringRooms);
            }
            
            using (StreamWriter file = File.CreateText(_stockRoomFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _stockRoom);
            }
            
        }

        public OperatingRoom OperatingRoomById(string id)
        {
            foreach (var room in _operatingRooms)
            {
                if (room.Id == id)
                    return room;
            }
            return null;

        }
        public OverviewRoom OverviewRoomById(string id) {
        foreach (var room in _overviewRooms)
        {
                if (room.Id == id)
                    return room;
        }
        return null;
        }

        public RetiringRoom RetiringRoomById(string id)
        {
            foreach (var room in _retiringRooms)
            {
                if (room.Id == id)
                    return room;
            }

            return null;
        }
        
        public OverviewRoom FindOverviewRoom()
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

        public OperatingRoom FindOperatingRoom()
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

        public RetiringRoom FindRetiringRoom()
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
                Console.Write("Choose the number to see operating room: >>  ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }

        public  void ViewOverviewRooms(OverviewRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + room.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {
                room.PrintRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager._overviewRooms.Remove(room);
                        break;
                    case "2":
                        ChangeOvRoom(room);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public void ViewOperatingRooms(OperatingRoom operatingRoom)
        {
            // proveriti da li se trenutno renovira
            if (!operatingRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + operatingRoom.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {
                operatingRoom.PrintRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager._operatingRooms.Remove(operatingRoom);
                        break;
                    case "2":
                        ChangeOpRoom(operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        private static string GetOption()
        {
            Console.WriteLine("Choose option or any for exit: ");
            Console.WriteLine("1) Delete room");
            Console.WriteLine("2) Change room");
            Console.Write(">> ");
            var option = Console.ReadLine();
            return option;

        }

        private static void ViewRetiringRoom(RetiringRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + room.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {

                room.printRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager._retiringRooms.Remove(room);
                        break;
                    case "2":
                        ChangeRetiringRoom(room);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        private static void ChangeRetiringRoom(RetiringRoom retiring)
        {
            if (!retiring.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + retiring.TimeOfRenovation.Value);
                return;
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

        private static void ChangeOvRoom(OverviewRoom overview)
        {
            if (!overview.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + overview.TimeOfRenovation.Value);
                return;
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
                        ChangeFurnitureInOvRoom(overview);
                        break;
                        
                    case "3":
                        ChangeMedicalTools(overview);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
        private static void ChangeMedicalTools(OverviewRoom overviewRoom)
        {
            if (!overviewRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + overviewRoom.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {
                Console.WriteLine("Choose option or x for exit ");
                Console.WriteLine("1) Add new medical tool from stock room");
                Console.WriteLine("2) Add new medical tool from another overview room");
                Console.WriteLine("3) Remove current medical tool");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewMedicalToolFromStockRoom(overviewRoom);
                        break;
                    case "3":
                        RemoveMedicalTool(overviewRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        private static void AddNewMedicalToolFromStockRoom(OverviewRoom overviewRoom)
        {
            Dictionary<MedicalTool, int> dict = new Dictionary<MedicalTool, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 1; j < 6; j++)
            {
                string shadeName = ((MedicalTool) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());
            if (_manager.RoomManager._stockRoom.MedicalEquipment[(MedicalTool) choice] >= num)
            {
                var time = GetTime();
                dict[(MedicalTool) choice] = num;
                Timer timer = new Timer(time, overviewRoom.Id);
                timer.MedicalDict = dict;
                _manager.TimerManager.Timers.Add(timer);

            }
            else
            {
                Console.WriteLine("Stock room just have " +
                                  _manager.RoomManager._stockRoom.MedicalEquipment[(MedicalTool) choice] + " " +
                                  ((MedicalTool) choice) + "s.");
            }

        }

        public static DateTime GetTime()
        {
            Console.WriteLine("Input year >> ");
            int year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input month >> ");
            int month = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input day >> ");
            int day= Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input hour >> ");
            int hour = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input minute >> ");
            int minute = Int32.Parse(Console.ReadLine());
            return new DateTime(year, month, day, hour, minute, 0);
        }

        private static void RemoveMedicalTool(OverviewRoom overviewRoom)
        {
            Dictionary<MedicalTool, int> dict = new Dictionary<MedicalTool, int>();
            Console.WriteLine("Choose what you want to remove: ");
            foreach (var tools in overviewRoom.Tools)
                Console.WriteLine(tools.Key + ") " + tools.Value);
            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to remove? >> ");
            int num = int.Parse(Console.ReadLine());
            if (overviewRoom.Tools[(MedicalTool) choice] > num)
            {
                var time = GetTime();
                dict[(MedicalTool) choice] = -num;
                Timer timer = new Timer(time, overviewRoom.Id);
                timer.MedicalDict = dict;
                _manager.TimerManager.Timers.Add(timer);
            }
            else
                Console.WriteLine("You don’t have that much equipment");
        }

        private static void ChangeOpRoom(OperatingRoom operatingRoom)
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
                        ChangeFurnitureInOpRoom(operatingRoom);
                         break;
                    case "3":
                        ChangeSurgeryTools(operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        private static void ChangeFurnitureInOpRoom(OperatingRoom operatingRoom)
        {
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Add new furniture");
                Console.WriteLine("2) Remove current furniture");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewFurniture(operatingRoom);
                        break;
                    case "2":
                        RemoveCurrentFurniture(operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
        private static void ChangeFurnitureInOvRoom(OverviewRoom overviewRoom)
        {
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Add new furniture");
                Console.WriteLine("2) Remove current furniture");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewFurniture(overviewRoom);
                        break;
                    case "2":
                        RemoveCurrentFurniture(overviewRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        private static void AddNewFurniture(OperatingRoom operatingRoom)
        {
            Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 0; j <4; j++)
            {
                string shadeName = ((Furniture) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());
            
            if (_manager.RoomManager._stockRoom.Furniture[(Furniture) choice] >= num)
            {
                var time = GetTime();
                dict[(Furniture) choice] = num;
                Timer timer = new Timer(time, operatingRoom.Id);
                timer.FurnitureDict = dict;
                _manager.TimerManager.Timers.Add(timer);
            }
            else
            {
                Console.WriteLine("Stock room just have " + 
                                  _manager.RoomManager._stockRoom.Furniture[(Furniture) choice] + " " +
                                  ((Furniture) choice).ToString() + "s.");
            }
           
        }
        
        private static void AddNewFurniture(OverviewRoom overviewRoom)
        {
            Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 0; j <4; j++)
            {
                string shadeName = ((Furniture) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());
            
            if (_manager.RoomManager._stockRoom.Furniture[(Furniture) choice] >= num)
            {
                var time = GetTime();
                dict[(Furniture) choice] = num;
                Timer timer = new Timer(time, overviewRoom.Id);
                timer.FurnitureDict = dict;
                _manager.TimerManager.Timers.Add(timer);
            }
            else
            {
                Console.WriteLine("Stock room just have " + 
                                  _manager.RoomManager._stockRoom.Furniture[(Furniture) choice] + " " +
                                  ((Furniture) choice).ToString() + "s.");
            }
           
        }

        private static void RemoveCurrentFurniture(OperatingRoom operatingRoom)
        {
            Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
            Console.WriteLine("Choose what you want to remove: ");
            int i = 1;
            int choice;
            Dictionary<int, Furniture> checkDict = new Dictionary<int, Furniture>();
            while (true)
            {
                foreach (var tools in operatingRoom.Furniture)
                {
                    checkDict[i] = tools.Key;
                    Console.WriteLine(i + ") " + tools.Key + "  Capacity: " + tools.Value);
                    i++;
                }

                Console.WriteLine(">> ");
                choice = int.Parse(Console.ReadLine());
                if (!checkDict.ContainsKey(choice))
                    Console.WriteLine("Wrong input");
                else
                    break;
            }
        }
        
        private static void RemoveCurrentFurniture(OverviewRoom overviewRoom)
        {
            Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
            Console.WriteLine("Choose what you want to remove: ");
            int i = 1;
            int choice;
            Dictionary<int, Furniture> checkDict = new Dictionary<int, Furniture>();
            while (true)
            {
                foreach (var tools in overviewRoom.Furniture)
                {
                    checkDict[i] = tools.Key;
                    Console.WriteLine(i + ") " + tools.Key + "  Capacity: " + tools.Value);
                    i++;
                }

                Console.WriteLine(">> ");
                choice = int.Parse(Console.ReadLine());
                if (!checkDict.ContainsKey(choice))
                    Console.WriteLine("Wrong input");
                else
                    break;
            }
        }
        private static void ChangeSurgeryTools(OperatingRoom operatingRoom)
        {
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Add new surgery tool");
                Console.WriteLine("2) Remove current surgery tool");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewSurgeryTool(operatingRoom);
                        break;
                    case "2":
                        RemoveSurgeryTool(operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        private static void AddNewSurgeryTool(OperatingRoom operatingRoom)
        {
            Dictionary<SurgeryTool, int> dict = new Dictionary<SurgeryTool, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 0; j <7; j++)
            {
                string shadeName = ((SurgeryTool) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());

            if (_manager.RoomManager._stockRoom.SurgeryEquipment[(SurgeryTool) choice] >= num)
            {
                var time = GetTime();
                dict[(SurgeryTool) choice] = num;
                Timer timer = new Timer(time, operatingRoom.Id);
                timer.SurgeryDict = dict;
                _manager.TimerManager.Timers.Add(timer);

            }
            else
            {
                Console.WriteLine("Stock room just have " + 
                                  _manager.RoomManager._stockRoom.SurgeryEquipment[(SurgeryTool) choice] + " " +
                                  ((SurgeryTool) choice) + "s.");
            }
        }
        
        private static void RemoveSurgeryTool(OperatingRoom operatingRoom)
        {
            Dictionary<SurgeryTool, int> dict = new Dictionary<SurgeryTool, int>();
            Console.WriteLine("Choose what you want to remove: ");
            int i = 1;
            int choice;
            Dictionary<int, SurgeryTool> checkDict = new Dictionary<int, SurgeryTool>();
            while (true)
            {
                foreach (var tools in operatingRoom.SurgeryEquipments)
                {
                    checkDict[i] = tools.Key;
                    Console.WriteLine(i + ") " + tools.Key + "  Capacity: " + tools.Value);
                    i++;
                }
                Console.WriteLine(">> ");
                choice = int.Parse(Console.ReadLine());
                if (!checkDict.ContainsKey(choice))
                    Console.WriteLine("Wrong input");
                else
                    break;
            }

            Console.WriteLine("How much you want to remove? >> ");
            int num = int.Parse(Console.ReadLine());
           
            if (operatingRoom.SurgeryEquipments[checkDict[choice]] > num)
            {
                var time = GetTime();
                dict[(SurgeryTool) choice] = -num;
                Timer timer = new Timer(time, operatingRoom.Id);
                timer.SurgeryDict = dict;
                _manager.TimerManager.Timers.Add(timer);
            }
            else
                Console.WriteLine("You don’t have that much equipment");
        }

        
        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
        public List<OverviewRoom> OverviewRooms
        {
            get => _overviewRooms;
        }
        public List<OperatingRoom> OperatingRooms
        {
            get => _operatingRooms;
        }
        public List<RetiringRoom> RetiringRooms
        {
            get => _retiringRooms;
        }
    }
}