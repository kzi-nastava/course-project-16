using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Usi_Project.Repository.RoomRepository
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