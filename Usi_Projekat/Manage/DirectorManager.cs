using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Usi_Projekat.Users;
using System.IO;
namespace Usi_Projekat.Manage
{
    public class DirectorManager
    {
        private string _directorFilename;
        private Director _director;
        private Factory _manager;
        private RoomManager _roomManager;

        public DirectorManager(string directorFilename, Factory factory)
        {
            _directorFilename = directorFilename;
            _manager = factory;
            _roomManager = new RoomManager();
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _director = JsonConvert.DeserializeObject<Director>(File.ReadAllText(_directorFilename), json);
        }

        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - View hospital rooms.");
                Console.WriteLine("x) - Exit.");
                
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewHospitalRooms();
                        break;

                    case "2":
                        //ChangingPatientProfile(factory);
                        break;

                    case "3":
                        //DeletingPatientProfile(factory);
                        break;

                    case "4":
                        // BlockingPatientProfile(factory);
                        break;

                    case "x":
                        break;

                    default:
                        Console.WriteLine("Invalid option entered, try again");
                        Menu();
                        break;
                }

            }
        }

        public static void  ViewHospitalRooms()
        {
            
            
        }
        public Director CheckPersonalInfo(string email, string password)
        {
            if (email == _director.email && password == _director.password)
            {
                return _director;
            }
            return null;
        }
        public bool CheckEmail(string email)
        {
            return (email == _director.email);
            if (email == _director.email)
            {
                return true;
            }

            return false;
        }
        
        public bool CheckPassword(string password)
        {
            if (password == _director.password)
            {
                return true;
            }
            return false;

        }
    }
}