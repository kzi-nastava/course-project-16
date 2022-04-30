namespace Usi_Projekat.Users
{
    public class Director : User
    {
                        
        public Director(string email, string password, string name, string lastName, string adress, string phone)
            : base(email, password, name, lastName, adress, phone, Role.Director) {}
        public Director()
        {
        }
 
    }
}