using Newtonsoft.Json;
using System;
using System.IO;
using Usi_Project.Manage;

namespace Usi_Project.Users
{
    public class Secretary : User
    {
        public Secretary(string email, string password, string name, string lastName, string address, string phone)
            : base(email, password, name, lastName, address, phone, Role.Secretary)
        {
        }
    }
    }