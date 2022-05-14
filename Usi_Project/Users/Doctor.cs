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

        public string Specialisation
        {
            get => _specialisation;
            set => _specialisation = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string Adress
        {
            get => adress;
            set => adress = value;
        }

        public string Phone
        {
            get => phone;
            set => phone = value;
        }

        public Role Role
        {
            get => role;
            set => role = value;
        }
    }
    
    
}