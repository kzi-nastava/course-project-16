namespace Usi_Projekat.Doctor
{
    public class Patient : User
    {
        private int blocked;
        
        public Patient(string email, string password, string name, string lastName, string adress, string phone, string id)
            : base(email, password, name, lastName, adress, phone, id, Role.Patient)
        {
            this.blocked = 0;
        }
        public int Blocked
        {
            get => blocked;
            set => blocked = value;
        }
    }
}