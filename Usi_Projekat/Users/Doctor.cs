using System.Collections.Generic;
using System;

namespace Usi_Projekat.Users
{
    public class Doctor:User
    {
        public List<OperatingRoom> operating;
        public List<OverviewRoom> overview;
        
        public Doctor(string email, string password, string name, string lastName, string adress, string phone)
            : base(email, password, name, lastName, adress, phone, Role.Doctor)
        {
            operating = new List<OperatingRoom>();
            overview = new List<OverviewRoom>();
        }
    }
    
    
}