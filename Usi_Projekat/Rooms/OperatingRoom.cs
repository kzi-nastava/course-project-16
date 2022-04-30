using System.Collections.Generic;
namespace Usi_Projekat

{
    public class OperatingRoom : HospitalRoom
    {
        private Dictionary<SurgeryTool, int> _surgeryEquipments;

        public OperatingRoom()
        {
            _surgeryEquipments = new Dictionary<SurgeryTool, int>();
        }

        public OperatingRoom(string id, string name, Dictionary<Furniture, int> furniture, Dictionary<SurgeryTool, int> surgeryEquipments) 
            : base(id, name, furniture)
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
    }
}