namespace Usi_Projekat
{
    public class StockRoom : HospitalRoom
    {
        private SurgeryEquipment _surgeryEquipment;
        private MedicalEquipment _medicalEquipment;
        
        public StockRoom() {}

        public SurgeryEquipment SurgeryEquipment
        {
            get => _surgeryEquipment;
            set => _surgeryEquipment = value;
        }

        public MedicalEquipment MedicalEquipment
        {
            get => _medicalEquipment;
            set => _medicalEquipment = value;
        }

        public StockRoom(string id, string name) : base(id, name) {}

        public StockRoom(string id, string name, RoomFurniture furniture, SurgeryEquipment surgeryEquipment, MedicalEquipment medicalEquipment) : base(id, name, furniture)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
        }

        protected StockRoom(SurgeryEquipment surgeryEquipment, MedicalEquipment medicalEquipment)
        {
            _surgeryEquipment = surgeryEquipment;
            _medicalEquipment = medicalEquipment;
        }
        
    }
}