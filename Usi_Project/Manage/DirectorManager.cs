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
                Console.WriteLine("1) - View hospital rooms");
                Console.WriteLine("2) - Create hospital rooms");
                Console.WriteLine("3) - Search hospital equipments");
                Console.WriteLine("x) - Exit.");
                Console.WriteLine(">> ");
                
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewHospitalRooms(_factory);
                        break;
                    case "2":
                        CreateHospitalRooms();
                        break;
                    case "3":
                        SearchHospitalEquipments();
                        break;
                    case "x":
                        return;
                    case "r":
                        _factory.TimerManager.RefreshEquipments();
                        break;
                    default:
                        Console.WriteLine("Invalid option entered, try again");
                        Menu();
                        break;
                }

            }
        }

        private static void PrintRoomsMenu()
        {
            
            Console.WriteLine("Choose type of hospital room: ");
            Console.WriteLine("1) Operating room");
            Console.WriteLine("2) Overview room");
            Console.WriteLine("3) Retiring room");
            Console.WriteLine("4) Stock room");
            Console.WriteLine("5) Ok");
            Console.WriteLine(">> ");
        }

        private static void PrintQuantityMenu()
        {
            Console.WriteLine("Choose quantity of equipment: ");
            Console.WriteLine("1) Out of stock");
            Console.WriteLine("2) 0-10");
            Console.WriteLine("3) 10+");
            Console.WriteLine("4) Ok");
            Console.WriteLine(">> ");
        }

        private static void SearchHospitalEquipments()
        {
            Console.WriteLine("Enter equipment: >> ");
            string eq = Console.ReadLine();
            List<int> options = new List<int>();
            while (!options.Contains(5))
            {
                PrintRoomsMenu();
                options.Add(Int32.Parse(Console.ReadLine()));
            }
            PrintQuantityMenu();
            int option2 = Int32.Parse(Console.ReadLine());
            Search(eq, options, option2);
        }

        private static void Search(string eq, List<int> options1, int option2)
        {
            foreach (var q in options1)
            {
                if (q == 1)
                {
                    SearchThroughOperatingRooms(eq.ToLower(), option2);
                }
                else if (q == 2)
                    SearchThroughOverviewRooms(eq.ToLower(), option2);
                else if (q == 3)
                    SearchThroughRetiringRooms(eq.ToLower(), option2);
                else if (q == 4)
                    SearchThroughStockRoom(eq.ToLower(), option2);
            }
        }

        private static void SearchThroughOperatingRooms(string eq, int option2)
        {
            foreach (OperatingRoom op in _factory.RoomManager.OperatingRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");

                foreach (var dictionary in op.SurgeryEquipments)
                {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
                foreach (var dictionary in op.Furniture)
                {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
            }
        }
        //
        private static void SearchThroughOverviewRooms(string eq, int option2)
        {
            foreach (OverviewRoom op in _factory.RoomManager.OverviewRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                foreach (var dictionary in op.Tools)
                {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
                foreach (var dictionary in op.Furniture)
                {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
            }
        }
        
        private static void SearchThroughRetiringRooms(string eq, int option2)
        {
            foreach (RetiringRoom op in _factory.RoomManager.RetiringRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                foreach (var dictionary in op.Furniture)
                {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
            }
        }
        
        private static void SearchThroughStockRoom(string eq, int option2)
        {
            foreach (var dictionary in _factory.RoomManager.StockRoom.MedicalEquipment)
            {
                    if (option2 == 1)
                    {   
                        if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 2)
                    {   
                        if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                    else if (option2 == 3)
                    {   
                        if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                            Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                    }
                }
            foreach (var dictionary in _factory.RoomManager.StockRoom.SurgeryEquipment)
            {
                if (option2 == 1)
                {   
                    if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
                else if (option2 == 2)
                {   
                    if (dictionary.Value <= 10 && dictionary.Value >= 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : " + dictionary.Value);
                }
                else if (option2 == 3)
                {   
                    if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
            }
            foreach (var dictionary in _factory.RoomManager.StockRoom.Furniture)
            {
                if (option2 == 1)
                {   
                    if (dictionary.Value == 0&& dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
                else if (option2 == 2 )
                {   
                    if (dictionary.Value <= 10 && dictionary.Value >= 0 && dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
                else if (option2 == 3)
                {   
                    if (dictionary.Value > 10&& dictionary.Key.ToString().ToLower().Contains(eq))
                        Console.WriteLine(dictionary.Key.ToString() + " : "  + dictionary.Value);
                }
            }
            
        }
        

        private static void CreateHospitalRooms()
        {
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Create Overview room");
                Console.WriteLine("2) - Create Operating room");
                Console.WriteLine("3) - Create Retiring room");
                Console.WriteLine("x) - Back");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        CreateOverviewRoom();
                        break;
                    case "2":
                        CreateOperatingRoom();
                        break;
                    case "3":
                        CreateRetiringRoom();
                        break;
                    case "x":
                        return;
                    default:
                    {
                        Console.WriteLine("Wrong input.");
                        continue;
                    }

                }
            }

        }

        public static void CreateOverviewRoom()
        {
            while (true)
            {
                bool ind = false;
                Console.WriteLine("Input id of new Overview Room >> ");
                string id = Console.ReadLine();
                foreach (OverviewRoom ov in _factory.RoomManager.OverviewRooms)
                {
                    if (ov.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                        ind = true;
                        break;
                    }
                }
                if (ind) continue;
                Console.WriteLine("Input name of new Overview room >> ");
                string name = Console.ReadLine();
                OverviewRoom overviewRoom = new OverviewRoom(id, name);
                _factory.RoomManager.OverviewRooms.Add(overviewRoom);
                break;
            }   
        }

        public static void CreateOperatingRoom()
        {  while (true)
            {
                Console.WriteLine("Input id of new Operating Room >> ");
                string id = Console.ReadLine();
                foreach (OperatingRoom ov in _factory.RoomManager.OperatingRooms)
                {
                    if (ov.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }
                Console.WriteLine("Input name of new Operating room >> ");
                string name = Console.ReadLine();
                OperatingRoom ovpRoom= new OperatingRoom(id, name);
                _factory.RoomManager.OperatingRooms.Add(ovpRoom);
                break;
            }   
            
        }

        public static void CreateRetiringRoom()
        {
            while (true)
            {
                Console.WriteLine("Input id of new Retiring Room >> ");
                string id = Console.ReadLine();
                foreach (RetiringRoom ov in _factory.RoomManager.RetiringRooms)
                {
                    if (ov.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }
                Console.WriteLine("Input name of new Retiring room >> ");
                string name = Console.ReadLine();
                RetiringRoom retiring = new RetiringRoom(id, name);
                _factory.RoomManager.RetiringRooms.Add(retiring);
                break;
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