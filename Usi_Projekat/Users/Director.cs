namespace Usi_Projekat.Doctor
{
    public class Boss : User
    {
                        
        public Boss(string email, string password, string name, string lastName, string adress, string phone, string id)
            : base(email, password, name, lastName, adress, phone, id, Role.Director) {}
    }
}