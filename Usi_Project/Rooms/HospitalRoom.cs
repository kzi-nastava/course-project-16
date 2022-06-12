using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Usi_Project
{
    public abstract class HospitalRoom
    {
        private string _id;
        private string _name;
        private bool _forRemove;
        private Dictionary<Furniture, int> _furniture;
        private Dictionary<DynamicEquipment, int> _dynamicTool;
        private KeyValuePair<DateTime, DateTime> _timeOfRenovation;

        public HospitalRoom()
        {
            _id = "";
            _name = "";
            _forRemove = false;
            _furniture = new Dictionary<Furniture, int>();
            _dynamicTool = new Dictionary<DynamicEquipment, int>();
            _timeOfRenovation = new KeyValuePair<DateTime, DateTime>();

        }

        public KeyValuePair<DateTime, DateTime> TimeOfRenovation
        {
            get => _timeOfRenovation;
            set => _timeOfRenovation = value;
        }

        public bool ForRemove
        {
            get => _forRemove;
            set => _forRemove = value;
        }

        public Dictionary<Furniture, int> Furniture
        {
            get => _furniture;
            set => _furniture = value;
        }
        
        public Dictionary<DynamicEquipment, int> DynamicEquipment
        {
            get => _dynamicTool;
            set => _dynamicTool = value;
        }

        public HospitalRoom(string id, string name, Dictionary<Furniture, int> furniture, Dictionary<DynamicEquipment, int> dynamicTool)
        {
            _id = id;
            _name = name;
            _furniture = furniture;
            _dynamicTool = dynamicTool;
        }

        public HospitalRoom(string id, string name, Dictionary<Furniture, int> furniture)
        {
            _id = id;
            _name = name;
            _furniture = furniture;
        }

        public HospitalRoom(string id, string name)
        {
            _id = id;
            _name = name;
            _furniture = new Dictionary<Furniture, int>();
        }

        public string Id
        {
            get => _id;
            set =>_id = value;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public virtual void PrintRoom()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("ID: " + Id); 
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Furniture:");
            Console.WriteLine("-------------------------");
            foreach (var tools in Furniture)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("-------------------------");
        }

        public bool IsDateTimeOfRenovationDefault()
        {
            return (TimeOfRenovation.Key == default);
        }

        public void PrintFurniture(string equipment, int parameterOfSearch)
        {
            foreach (var dictionary in Furniture)
            {
                if (parameterOfSearch == 1)
                {   
                    if (dictionary.Value == 0 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 2)
                {   
                    if (dictionary.Value <= 10 && dictionary.Value >= 0
                                               && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 3)
                {   
                    if (dictionary.Value > 10 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
            }
        }
        
        public Dictionary<DynamicEquipment, int> PrintDynamicTools(Dictionary<DynamicEquipment, int> dict )
        {
            foreach (var dictionary in DynamicEquipment)
            {
                if (dict[dictionary.Key]== 0)
                {
                    dict[dictionary.Key] = dictionary.Value;
                }
                else
                {
                    dict[dictionary.Key] = dict[dictionary.Key] + dictionary.Value;
                }
            }
            return dict;
        }

        public List<string> PrintLowSupplyEquipment(List<string> IdOfRooms)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Room: " + _id);
            IdOfRooms.Add(_id);
            foreach (var dictionary in DynamicEquipment)
            {
                
                if (dictionary.Value == 0 )
                    Console.WriteLine(dictionary.Key.ToString() + " OUT OF STOCK! ");
                else if (dictionary.Value < 5 )
                    Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
            }

            return IdOfRooms;

        }

        public void PrintEquipment()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Room: " + _id);
            foreach (var dictionary in DynamicEquipment)
            {
                Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
            }

        }
       
    }
}