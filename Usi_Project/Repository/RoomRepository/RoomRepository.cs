using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using Usi_Project.Settings;
using Usi_Project;

namespace Usi_Project.Repository.RoomRepository
{
    public class RoomRepository :  IRoomRepository
    {
        
        private StockRoom _stockRoom;
        private readonly FileSettings _fileSettings;
        private List<OverviewRoom> _overviewRooms;
        private List<OperatingRoom> _operatingRooms;
        private List<RetiringRoom> _retiringRooms;
        
        public RoomRepository(FileSettings fileSettings)
        {
            _fileSettings = fileSettings;
            _operatingRooms = new List<OperatingRoom>();
            _overviewRooms = new List<OverviewRoom>();
            _retiringRooms = new List<RetiringRoom>();
            
        }
        
        public OperatingRoom GetOperatingRoomById(string id)
        {
            foreach (var room in _operatingRooms)
            {
                if (room.Id == id)
                    return room;
            }
            return null;

        }
        public OverviewRoom GetOverviewRoomById(string id) {
        foreach (var room in _overviewRooms)
        {
                if (room.Id == id)
                    return room;
        }
        return null;
        }

        public RetiringRoom GetRetiringRoomById(string id)
        {
            foreach (var room in _retiringRooms)
            {
                if (room.Id == id)
                    return room;
            }

            return null;
        }

        public void AddRetiringRoom(RetiringRoom room)
        {
            _retiringRooms.Add(room);
        }

        public void AddOperatingRoom(OperatingRoom room)
        {
            _operatingRooms.Add(room);
        }

        public void AddOverviewRoom(OverviewRoom room)
        {
            _overviewRooms.Add(room);
        }

        public void DeleteRetiringRoom(RetiringRoom room)
        {
            _retiringRooms.Remove(room);
        }

        public void DeleteOperatingRoom(OperatingRoom room)
        {
            _operatingRooms.Remove(room);
        }

        public void DeleteOverviewRoom(OverviewRoom room)
        {
            _overviewRooms.Remove(room);
        }

        public void UpdateRetiringRoom(RetiringRoom room)
        {
            var findRoom = GetRetiringRoomById(room.Id);
            findRoom.Name = room.Name;
        }

        public void LoadData()
        {
            
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            
            _overviewRooms = JsonConvert.DeserializeObject<List<OverviewRoom>>(File.ReadAllText(_fileSettings.OverviewRoomsFilename), json);
            _operatingRooms = JsonConvert.DeserializeObject<List<OperatingRoom>>(File.ReadAllText(_fileSettings.OperatingRoomsFilename), json);
            _retiringRooms = JsonConvert.DeserializeObject<List<RetiringRoom>>(File.ReadAllText(_fileSettings.RetiringRoomsFilename), json);
            _stockRoom = JsonConvert.DeserializeObject<StockRoom>(File.ReadAllText(_fileSettings.StockRoomFilename), json);
        }

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_fileSettings.OverviewRoomsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _overviewRooms);
            }
            
            using (StreamWriter file = File.CreateText(_fileSettings.OverviewRoomsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _operatingRooms);
            }
            
            using (StreamWriter file = File.CreateText(_fileSettings.RetiringRoomsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _retiringRooms);
            }
            
            using (StreamWriter file = File.CreateText(_fileSettings.StockRoomFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _stockRoom);
            }
            
        }
        
        public StockRoom StockRoom
        {
            get => _stockRoom;
        }
        
        public List<OverviewRoom> OverviewRooms
        {
            get { return _overviewRooms;  }
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