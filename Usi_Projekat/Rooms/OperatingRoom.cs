namespace Usi_Projekat
{
    public class OperatingRoom : HospitalRoom
    {
        private SurgeryEquipment _surgeryEquipment;

        protected OperatingRoom(SurgeryEquipment surgeryEquipment)
        {
            _surgeryEquipment = surgeryEquipment;
        }

        public OperatingRoom(string id, string name, RoomFurniture furniture, SurgeryEquipment surgeryEquipment) : base(id, name, furniture)
        {
            _surgeryEquipment = surgeryEquipment;
        }
        
        public OperatingRoom()
        {
            _surgeryEquipment = new SurgeryEquipment();
        }


        public SurgeryEquipment SurgeryEquipment
        {
            get => _surgeryEquipment;
            set => _surgeryEquipment = value;
        }
    }
}