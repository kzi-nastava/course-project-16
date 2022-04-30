using System;
using System.Collections.Generic;

namespace Usi_Project
{
    public class Timer
    {
        private DateTime _dateTime;
        private string _idRoom;
        private Dictionary<MedicalTool, int> _medicalDict;
        private Dictionary<SurgeryTool, int> _surgeryDict;
        private Dictionary<Furniture, int> _furnitureDict;

        public Timer(DateTime time, string id)
        {
            _idRoom = id;
            _dateTime = time;
            _medicalDict = new Dictionary<MedicalTool, int>();
            _surgeryDict = new Dictionary<SurgeryTool, int>();
            _furnitureDict = new Dictionary<Furniture, int>();
        }
        public Timer()
        {
            _dateTime = new DateTime();
            _medicalDict = new Dictionary<MedicalTool, int>();
            _surgeryDict = new Dictionary<SurgeryTool, int>();
            _furnitureDict = new Dictionary<Furniture, int>();
        }

        public Dictionary<MedicalTool, int> MedicalDict
        {
            get => _medicalDict;
            set => _medicalDict = value;
        }

        public Dictionary<SurgeryTool, int> SurgeryDict
        {
            get => _surgeryDict;
            set => _surgeryDict = value;
        }

        public Dictionary<Furniture, int> FurnitureDict
        {
            get => _furnitureDict;
            set => _furnitureDict = value;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => _dateTime = value;
        }

        public string IdRoom
        {
            get => _idRoom;
            set => _idRoom = value;
        }
    }
}