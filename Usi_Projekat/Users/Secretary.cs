namespace Usi_Projekat.Doctor
{
    public class Secretary : User
    {
                
        public Secretary(string email, string password, string name, string lastName, string adress, string phone, string id)
            : base(email, password, name, lastName, adress, phone, id, Role.Secretary) {}
    }
}