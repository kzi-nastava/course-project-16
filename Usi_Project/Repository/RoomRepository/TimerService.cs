using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class TimerManager
    {
        private string _timerFilename;
        private static List<Timer> _timers;
        private static Factory _factory;

        public TimerManager(string timerFilename, Factory factory)
        {
            _timerFilename = timerFilename;
            _factory = factory;
            _timers = new List<Timer>();
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _timers = JsonConvert.DeserializeObject<List<Timer>>(File.ReadAllText(_timerFilename), json);
        }

        public void RefreshEquipments()
        {
            RefreshMedicalEquipments();
            RefreshSurgeryEquipments();
            RefreshFurniture();
            
        }
        
        private void RefreshMedicalEquipments()
        {
            foreach (var timer in _timers)
            {
                foreach (var med in timer.MedicalDict)
                {   
                    DateTime now = DateTime.Now;
                    if (now > timer.DateTime)
                    {
                        OverviewRoom overviewRoom = _factory.RoomRepository.GetOverviewRoomById(timer.IdRoom);
                        if (med.Value  > 0)
                        {
                            if (overviewRoom.Tools.ContainsKey(med.Key))
                                overviewRoom.Tools[med.Key] += med.Value;
                            else 
                                overviewRoom.Tools[med.Key] = med.Value;
                            _factory.RoomRepository.StockRoom.MedicalEquipment[med.Key] -= med.Value;
                        }
                        else
                        {
                            overviewRoom.Tools[med.Key] += med.Value;
                            _factory.RoomRepository.StockRoom.MedicalEquipment[med.Key] += (-1) * med.Value;
                        }   
                    }
                }
            }
        }
        
        private void RefreshFurniture()
        {
            foreach (var timer in _timers)
            {
                foreach (var med in timer.FurnitureDict)
                {   
                    DateTime now = DateTime.Now;
                    if (now > timer.DateTime)
                    {
                        OverviewRoom overviewRoom = _factory.RoomRepository.GetOverviewRoomById(timer.IdRoom);
                        if (overviewRoom != null)
                        {
                            if (med.Value > 0)
                            {
                                if (overviewRoom.Furniture.ContainsKey(med.Key))
                                    overviewRoom.Furniture[med.Key] += med.Value;
                                else 
                                    overviewRoom.Furniture[med.Key] = med.Value;
                                _factory.RoomRepository.StockRoom.Furniture[med.Key] -= med.Value;
                            }
                            else
                            {
                                overviewRoom.Furniture[med.Key] += med.Value;
                                _factory.RoomRepository.StockRoom.Furniture[med.Key] += (-1) * med.Value;
                            }
                        }
                        else
                            RefreshFurnitureOperatingRoom(med, timer);
                    }
                }
            }
        }

        private static void RefreshFurnitureOperatingRoom(KeyValuePair<Furniture,int> med, Timer timer)
        {
            OperatingRoom operatingRoom = _factory.RoomRepository.GetOperatingRoomById(timer.IdRoom);
            if (operatingRoom != null)
            {
                if (med.Value > 0)
                {
                    if (operatingRoom.Furniture.ContainsKey(med.Key))
                        operatingRoom.Furniture[med.Key] += med.Value;
                    else  
                        operatingRoom.Furniture[med.Key] = med.Value;
                    _factory.RoomRepository.StockRoom.Furniture[med.Key] -= med.Value;
                }
                else
                {
                    operatingRoom.Furniture[med.Key] += med.Value;
                    _factory.RoomRepository.StockRoom.Furniture[med.Key] += (-1) * med.Value;
                }
            }
            else RefreshFurnitureRetiringRoom(med, timer);
        }
        
        private static void RefreshFurnitureRetiringRoom(KeyValuePair<Furniture,int> med, Timer timer)
        {
            RetiringRoom retiringRoom = _factory.RoomRepository.GetRetiringRoomById(timer.IdRoom);
            if (med.Value > 0) 
            {
                if (retiringRoom.Furniture.ContainsKey(med.Key))
                    retiringRoom.Furniture[med.Key] += med.Value;
                else
                    retiringRoom.Furniture[med.Key] = med.Value;
                _factory.RoomRepository.StockRoom.Furniture[med.Key] -= med.Value; 
            }
            else
            {
                   retiringRoom.Furniture[med.Key] += med.Value;
                    _factory.RoomRepository.StockRoom.Furniture[med.Key] += (-1) * med.Value; 
            }
        }
    
        


        private void RefreshSurgeryEquipments()
        {
            foreach (var timer in _timers)
            {
                foreach (var med in timer.SurgeryDict)
                {
                    DateTime now = DateTime.Now;
                    if (now > timer.DateTime)
                    {
                        OperatingRoom operatingRoom = _factory.RoomRepository.GetOperatingRoomById(timer.IdRoom);
                        if (med.Value > 0)
                        {
                            if (operatingRoom.SurgeryEquipments.ContainsKey(med.Key))
                                operatingRoom.SurgeryEquipments[med.Key] += med.Value;
                            else 
                                operatingRoom.SurgeryEquipments[med.Key] = med.Value;
                            _factory.RoomRepository.StockRoom.SurgeryEquipment[med.Key] -= med.Value;
                        }
                        else
                        {
                            operatingRoom.SurgeryEquipments[med.Key] += med.Value;
                            _factory.RoomRepository.StockRoom.SurgeryEquipment[med.Key] += (-1) * med.Value;
                        }
                    }
                }
            }
        }


        // public void serialize()
        // {
        //
        //     using (StreamWriter file = File.CreateText("../../../Files/times.json"))
        //     {
        //         JsonSerializer serializer = new JsonSerializer();
        //         serializer.Formatting = Formatting.Indented;
        //         serializer.Serialize(file, _timers );
        //     }
        // }

        public string TimerFilename
        {
            get => _timerFilename;
            set => _timerFilename = value;
        }

        public List<Timer> Timers
        {
            get => _timers;
            set => _timers = value;
        }

        public Factory Factory
        {
            get => _factory;
            set => _factory = value;
        }
    }
    
}