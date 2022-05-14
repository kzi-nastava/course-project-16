using System;
using System.Collections.Generic;

namespace Usi_Project
{
    public abstract class HospitalRoom
    {
        private string _id;
        private string _name;
        private bool _forRemove;
        private Dictionary<Furniture, int> _furniture;
        private KeyValuePair<DateTime, DateTime> _timeOfRenovation;

        public HospitalRoom()
        {
            _id = "";
            _name = "";
            _forRemove = false;
            _furniture = new Dictionary<Furniture, int>();
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
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
        
    }
}