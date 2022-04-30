using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;

namespace Usi_Project.Manage
{
    public class DirectorManager
    {
        private string _directorFilename;
        private Director _director;
        private static Factory _factory;
        private RoomManager _roomManager;

        public DirectorManager(string directorFilename, Factory factory)
        {
            _directorFilename = directorFilename;
            _factory = factory;
            _roomManager = new RoomManager();
        }

        public string DirectorFilename
        {
            get => _directorFilename;
            set => _directorFilename = value;
        }

        public Director Director
        {
            get => _director;
            set => _director = value;
        }

        public Factory Factory
        {
            get => _factory;
            set => _factory = value;
        }

        public RoomManager RoomManager
        {
            get => _roomManager;
            set => _roomManager = value;
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
                        ViewHospitalRooms(_factory);
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

        public static void ViewHospitalRooms(Factory factory)
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1. View Operating rooms");
            Console.WriteLine("2. View Overview rooms");
            Console.WriteLine("3. View Stock room");
            Console.Write(">> ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                {
                    factory.RoomManager.ViewOperatingRooms();
                    break;
                }
                case "2":
                {
                    factory.RoomManager.ViewOverviewRooms();
                    break;
                }
                case "3":
                    break;
            }
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
        }
        
        public bool CheckPassword(string password)
        {
            return password == _director.password;

        }
    }
}