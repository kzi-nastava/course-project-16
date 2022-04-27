using System;

namespace Usi_Projekat
{
    public class User
    {
        public string email;
        public string password;
        public string name;
        public string lastName;
        public string adress;
        public string phone;
        public string id;
        public string role;

        public User()
        {
        }

        public User(string email, string password, string name, string lastName, string adress, string phone, string id, string role)
        {
            this.email = email;
            this.password = password;
            this.name = name;
            this.lastName = lastName;
            this.adress = adress;
            this.phone = phone;
            this.id = id;
            this.role = role;
        }

        public override string ToString()
        {
            return email;
        }
    }
    
}