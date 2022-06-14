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
            Dictionary<SurgeryTool, int> surgeryEquipment, Dictionary<MedicalTool, int> medicalEquipment) : base(id,
            name, furniture)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;

        }

        public StockRoom(string id, string name, Dictionary<Furniture, int> furniture, Dictionary<DynamicEquipment,
            int> dynamicTool, Dictionary<SurgeryTool, int> surgeryEquipment, Dictionary<MedicalTool, int> medicalEquipment) 
            : base(id, name, furniture, dynamicTool)
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

        public StockRoom(string id, string name, Dictionary<SurgeryTool, int> surgeryEquipment,
            Dictionary<MedicalTool, int> medicalEquipment) : base(id, name)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
        }

        public override void PrintRoom()
        {
            base.PrintRoom();
            Console.WriteLine("Medical Equipments:");
            Console.WriteLine("-------------------------");
            foreach (var tools in _medicalEquipment)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Surgery Equipments:");
            Console.WriteLine("-------------------------");
            foreach (var surgeryTools in _surgeryEquipment)
                Console.WriteLine("\t" + surgeryTools.Key + ": " + surgeryTools.Value);
            Console.WriteLine("=========================");
        }

        public void PrintMedicalEquipment(string equipment, int parameterOfSearch)
        {
            foreach (var dictionary in MedicalEquipment)
            {
                if (parameterOfSearch == 1)
                {
                    if (dictionary.Value == 0 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
                else if (parameterOfSearch == 2)
                {
                    if (dictionary.Value <= 10 && dictionary.Value >= 0 &&
                        dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
                else if (parameterOfSearch == 3)
                {
                    if (dictionary.Value > 10 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
            }
        }

        public void PrintSurgeryEquipment(string equipment, int parameterOfSearch)
        {
            foreach (var dictionary in SurgeryEquipment)
            {
                if (parameterOfSearch == 1)
                {
                    if (dictionary.Value == 0 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
                else if (parameterOfSearch == 2)
                {
                    if (dictionary.Value <= 10 && dictionary.Value >= 0 &&
                        dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
                else if (parameterOfSearch == 3)
                {
                    if (dictionary.Value > 10 && dictionary.Key.ToString().ToLower().Contains(equipment))
                        Console.WriteLine(dictionary.Key + " : " + dictionary.Value);
                }
            }

        }
    }
}