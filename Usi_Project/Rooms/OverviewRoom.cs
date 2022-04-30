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
        
        public void printRoom()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("ID: " + Id); 
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Medical Equipments:");
            Console.WriteLine("-------------------------");
            foreach (var tools in _tools)
            {
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("Furniture:");
            Console.WriteLine("-------------------------");
            foreach (var tools in Furniture)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("=========================");
  
            
        }
    }
    
}