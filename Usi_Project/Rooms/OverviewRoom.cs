using System.Collections.Generic;
using System;
namespace Usi_Project

{
    public class OverviewRoom : HospitalRoom
    {
        private Dictionary<MedicalTool, int> _tools;

        public OverviewRoom()
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public OverviewRoom(string id, string name) : base(id, name)
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public OverviewRoom(string id, string name, Dictionary<Furniture, int> furniture) : base(id, name, furniture)
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public OverviewRoom(Dictionary<MedicalTool, int> tools)
        {
            _tools = tools;
        }

        public OverviewRoom(string id, string name, Dictionary<MedicalTool, int> tools,  Dictionary<Furniture, int> furniture) 
            : base(id, name, furniture)
        {
            _tools = tools;
        }

        public Dictionary<MedicalTool, int> Tools
        {
            get => _tools;
            set => _tools = value;
        }
        
        public override void PrintRoom()
        {
            base.PrintRoom();
            Console.WriteLine("Medical Equipments:");
            Console.WriteLine("-------------------------");
            foreach (var tools in _tools)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("=========================");
        }

 
        public void PrintMedicalEquipments(string equipment, int parameterOfSearch)
        {
            foreach (var dictionary in Tools)
            {
                if (parameterOfSearch == 1)
                {   
                    if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 2)
                {   
                    if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString()
                                                                     .ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 3)
                {   
                    if (dictionary.Value > 10 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key  + " : "  + dictionary.Value);
                }
            }
        }
        
        
    }
    
}