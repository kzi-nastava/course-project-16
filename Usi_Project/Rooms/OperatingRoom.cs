using System;
using System.Collections.Generic;
namespace Usi_Project

{
    public class OperatingRoom : HospitalRoom
    {
        private Dictionary<SurgeryTool, int> _surgeryEquipments;

        public OperatingRoom() : base()
        {
            _surgeryEquipments = new Dictionary<SurgeryTool, int>();
        }

        public OperatingRoom(string id, string name, Dictionary<Furniture, int> furniture, Dictionary<SurgeryTool, int> surgeryEquipments) 
            : base(id, name, furniture)
        {
            _surgeryEquipments = surgeryEquipments;
        }

        public OperatingRoom(string id, string name, Dictionary<Furniture, int> furniture, Dictionary<DynamicEquipment, int> dynamicTool, Dictionary<SurgeryTool, int> surgeryEquipments)
            : base(id, name, furniture, dynamicTool)
        {
            _surgeryEquipments = surgeryEquipments;
        }

        public OperatingRoom(string id, string name) : base(id, name)
        {
            _surgeryEquipments = new Dictionary<SurgeryTool, int>();
        }

        public OperatingRoom(string id, string name, Dictionary<SurgeryTool, int> surgeryEquipments) : base(id, name)
        {
            _surgeryEquipments = surgeryEquipments;
        }
 
        public Dictionary<SurgeryTool, int> SurgeryEquipments
        {
            get => _surgeryEquipments;
            set => _surgeryEquipments = value;
        }

        public override void PrintRoom()
        {
            base.PrintRoom();
            Console.WriteLine("Surgery Equipments:");
            Console.WriteLine("-------------------------");
            foreach (var tools in _surgeryEquipments)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("=========================");
            
        }

        public void PrintSurgeryEquipments(string equipment,  int parameterOfSearch)
        {
            foreach (var dictionary in SurgeryEquipments)
            {
                if (parameterOfSearch == 1)
                {   
                    if (dictionary.Value == 0 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 2)
                {   
                    if (dictionary.Value <= 10 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key+ " : "  + dictionary.Value);
                }
                else if (parameterOfSearch == 3)
                {   
                    if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : "  + dictionary.Value);
                }
               
            }
        }

        
        
        
  
    }
}