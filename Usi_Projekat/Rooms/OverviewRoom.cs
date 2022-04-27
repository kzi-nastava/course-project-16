namespace Usi_Projekat
{
    public class OverviewRoom : HospitalRoom
    {
        private MedicalEquipment _medicalEquipment;
        
        protected OverviewRoom(string id, string name) : base(id, name) {}

        public OverviewRoom(string id, string name, RoomFurniture furniture, MedicalEquipment medicalEquipment) : base(id, name, furniture)
        {
            _medicalEquipment = medicalEquipment;
        }
        
        protected OverviewRoom(string id, string name, MedicalEquipment medicalEquipment) : base(id, name)
        {
            _medicalEquipment = medicalEquipment;
        }

        public MedicalEquipment MedicalEquipment
        {
            get => _medicalEquipment;
            set => _medicalEquipment = value;
        }


    }
    
}