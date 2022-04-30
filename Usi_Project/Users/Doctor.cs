using System.Collections.Generic;
using System;

namespace Usi_Project.Users
{
    public class Doctor:User
    {


        public Doctor()
        {
        }

        public Doctor(string email, string password, string name, string lastName, string adress, string phone)
            : base(email, password, name, lastName, adress, phone, Role.Doctor)
        {
        }
    }
    
    
}