using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;

namespace Usi_Projekat.Manage
{
    public class RoomManager
    {
        private readonly string _operatingRoomsFn;
        private readonly string _overviewRoomsFn;
        private readonly string _retiringRoomsFn;
        private Factory _manager;
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


        // public void seriazlize()
        // {
        //     using (StreamWriter file = File.CreateText(_retiringRoomsFn))
        //     {
        //         JsonSerializer serializer = new JsonSerializer();
        //         serializer.Formatting = Formatting.Indented;
        //         serializer.Serialize(file, _retiringRooms);
        //     }
        // }

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