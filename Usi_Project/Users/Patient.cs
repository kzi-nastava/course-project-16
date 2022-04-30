namespace Usi_Project.Users
{
    public class Patient : User
    {
        private int blocked;
       private MedicalRecord medicalRecord;
        public Patient(string email, string password, string name, string lastName, string adress, string phone, MedicalRecord medicalRecord)
            : base(email, password, name, lastName, adress, phone, Role.Patient)
        {
            this.blocked = 0;
            this.medicalRecord = medicalRecord;
        
        }
        
        public int Blocked
        {
            get => blocked;
            set => blocked = value;
        }

        public MedicalRecord MedicalRecord
        {
            get => medicalRecord;
        }
        
    }
}