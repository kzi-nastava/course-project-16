using System.Collections.Generic;
using System;
using System.Net.NetworkInformation;

namespace Usi_Project.Users
{
    public class Doctor:User
    {
        private string _specialisation;


        public Doctor()
        {
        }

        public Doctor(string email, string password, string name, string lastName, string adress, string phone,string specialisation)
            : base(email, password, name, lastName, adress, phone, Role.Doctor)
        {
            _specialisation = specialisation;
        }
        public Doctor(string email, string password, string name, string lastName, string adress, string phone)
            : base(email, password, name, lastName, adress, phone, Role.Doctor)
        {
            _specialisation = null;
        }
    }
    
    
}