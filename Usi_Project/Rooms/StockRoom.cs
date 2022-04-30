using System;
using System.Collections.Generic;

namespace Usi_Project
{
    public class StockRoom : HospitalRoom
    {
        
        private Dictionary<SurgeryTool, int> _surgeryEquipment;
        private Dictionary<MedicalTool, int> _medicalEquipment;

        public StockRoom()
        {
            _surgeryEquipment = new Dictionary<SurgeryTool, int>();
            _medicalEquipment = new Dictionary<MedicalTool, int>();
        }


        public StockRoom(string id, string name) : base(id, name)
        {
            _surgeryEquipment = new Dictionary<SurgeryTool, int>();
            _medicalEquipment = new Dictionary<MedicalTool, int>();
        }

        public StockRoom(string id, string name, Dictionary<Furniture, int> furniture,
            Dictionary<SurgeryTool, int> surgeryEquipment, Dictionary<MedicalTool, int> medicalEquipment) : base(id, name, furniture )
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
            
        }

        public StockRoom(Dictionary<SurgeryTool, int> surgeryEquipment, Dictionary<MedicalTool, int> medicalEquipment)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
        }

        public Dictionary<SurgeryTool, int> SurgeryEquipment
        {
            get => _surgeryEquipment;
            set => _surgeryEquipment = value;
        }

        public Dictionary<MedicalTool, int> MedicalEquipment
        {
            get => _medicalEquipment;
            set => _medicalEquipment = value;
        }

        public StockRoom(string id, string name, Dictionary<SurgeryTool, int> surgeryEquipment, Dictionary<MedicalTool, int> medicalEquipment) : base(id, name)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
        }
        public void printRoom()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("ID: " + Id); 
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Medical Equipments:");
            foreach (var tools in _medicalEquipment)
            {
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            }
            Console.WriteLine("Furniture:");
            foreach (var tools in Furniture)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            
            Console.WriteLine("=========================");
  
            
        }
        
    }
}