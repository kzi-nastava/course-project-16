namespace Usi_Projekat.Users
{
    public class Patient : User
    {
        private int blocked;
        
        public Patient(string email, string password, string name, string lastName, string adress, string phone)
            : base(email, password, name, lastName, adress, phone, Role.Patient)
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