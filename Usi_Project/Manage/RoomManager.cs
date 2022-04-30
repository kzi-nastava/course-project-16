using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;

namespace Usi_Project.Manage
{
    public class RoomManager
    {
        private readonly string _operatingRoomsFn;
        private readonly string _overviewRoomsFn;
        private readonly string _retiringRoomsFn;
        private static Factory _manager;
        private List<OverviewRoom> _overviewRooms;
        private List<OperatingRoom> _operatingRooms;
        private List<RetiringRoom> _retiringRooms;
        
        public RoomManager()
        {
            _operatingRooms = new List<OperatingRoom>();
            _overviewRooms = new List<OverviewRoom>();
            _retiringRooms = new List<RetiringRoom>();
        }
        public RoomManager(string operatingRoomsFn, string overviewRoomsFn, string retiringRoomsFn, Factory factory)
        {
            _operatingRoomsFn = operatingRoomsFn;
            _overviewRoomsFn = overviewRoomsFn;
            _retiringRoomsFn = retiringRoomsFn;
            _operatingRooms = new List<OperatingRoom>();
            _overviewRooms = new List<OverviewRoom>();
            _retiringRooms = new List<RetiringRoom>();
            _manager = factory;
            
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
            
        }

        public OperatingRoom OperatingRoomById(string id)
        {
            foreach (var room in _operatingRooms)
            {
                if (room.Id == id)
                {
                    return room;
                }
            }
            return null;

        }
        public OverviewRoom OverviewRoomById(string id) {
        foreach (var room in _overviewRooms)
        {
                if (room.Id == id)
                {
                    return room;
                }
        }
        return null;
        }

        public RetiringRoom RetiringRoomById(string id) {
            foreach (var room in _retiringRooms)
            {
                if (room.Id == id)
                {
                    return room;
                }
            }
            return null;
        }

        public void ViewOperatingRooms()
        {
            int i = 1;
            Dictionary<int, OperatingRoom> dictionary = new Dictionary<int, OperatingRoom>();
            foreach (var opp in _operatingRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i.ToString() + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.WriteLine("Choose the number to see operating room: ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                {
                    ViewOpRoom(dictionary[p]);
                    break;
                }
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }
        }
        
        public void ViewOverviewRooms()
        {
            int i = 1;
            Dictionary<int, OverviewRoom> dictionary = new Dictionary<int, OverviewRoom>();
            foreach (var opp in _overviewRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i.ToString() + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.WriteLine("Choose the number to see operating room: ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                {
                    ViewOvRoom(dictionary[p]);
                    break;
                }
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }
        }

        private static string GetOption()
        {
            Console.WriteLine("Choose option or x for exit:");
            Console.WriteLine("1) Delete room");
            Console.WriteLine("2) Change room");
            Console.Write(">> ");
            string option = Console.ReadLine();
            return option;

        }

        private static void ViewOpRoom(OperatingRoom room)
        {
           room.printRoom();
           switch (GetOption())
           {
               case "1":
               {
                   _manager.RoomManager._operatingRooms.Remove(room);
                   break;
               }
               case "2":
               {
                ChangeOpRoom(room);   
                break;
               }
               default:
                   return;
           }

        }
        
        private static void ViewOvRoom(OverviewRoom room)
        {
            room.printRoom();
            switch (GetOption())
            {
                case "1":
                {
                    _manager.RoomManager._overviewRooms.Remove(room);
                    break;
                }
                case "2":
                {
                    ChangeOvRoom(room);   
                    break;
                }
                default:
                    return;
            }

        }
        
        private static void ViewRetiringRoom(RetiringRoom room)
        {
            room.printRoom();
            switch (GetOption())
            {
                case "1":
                {
                    _manager.RoomManager._retiringRooms.Remove(room);
                    break;
                }
                case "2":
                {
                    ChangeRetiringRoom(room);   
                    break;
                }
                default:
                    return;
            }

        }
        
        private static void ChangeRetiringRoom(RetiringRoom operatingRoom)
        {
            //TODO: CHANGE ROOM AND EQUIPMENTS
        }

        private static void ChangeOvRoom(OverviewRoom operatingRoom)
        {
            //TODO: CHANGE ROOM AND EQUIPMENTS
        }

        private static void ChangeOpRoom(OperatingRoom operatingRoom)
        {
            //TODO: CHANGE ROOM AND EQUIPMENTS
        }
            
        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }

        public List<OverviewRoom> OverviewRooms
        {
            get => _overviewRooms;
            set => _overviewRooms = value;
        }

        public List<OperatingRoom> OperatingRooms
        {
            get => _operatingRooms;
            set => _operatingRooms = value;
        }

        public List<RetiringRoom> RetiringRooms
        {
            get => _retiringRooms;
            set => _retiringRooms = value;
        }
    }
}