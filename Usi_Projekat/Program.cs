using System;

using Usi_Projekat.Methods;
using Usi_Projekat.Users;

namespace Usi_Projekat
{
    class Program
    {
        static void Main(string[] args)
        {
           IOController.Factory  factory = new IOController.Factory();
           Utills utills = new Utills(factory);
           utills.readUsers("./../../../users.json", "", "", "");

            while (true)
            {
                var ind = 0;
                Console.WriteLine("Enter role:");
                Console.WriteLine("1) - Director");
                Console.WriteLine("2) - Doctor");
                Console.WriteLine("3) - Patient");
                Console.WriteLine("4) - Secretary");
                Console.WriteLine("Enter email:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                string role = Console.ReadLine();
                switch (role)
                {
                    case "1":
                    {
                        if (username == factory.director.email && password == factory.director.password)
                        {
                            Message();
                            // factory.director.printMenu();
                            break;
                        }
                    }
                        break;

                    case "2":
                        foreach (var doctor in factory.doctors)
                        {
                            if (username == doctor.email && password == doctor.password)
                            {
                                Message();
                                // doctor.printMenu();
                                break;
                            }
                        }

                        break;

                    case "3":
                        foreach (var patient in factory.patients)
                        {
                            if (username == patient.email && password == patient.password && patient.Blocked == 0)
                            {
                                Message();
                                // patient.PrintMenu();
                                Console.WriteLine(patient.name);
                                break;
                            }
                        }

                        break;

                    case "4":
                        foreach (var secretary in factory.secretaries)
                        {
                            if (username == secretary.email && password == secretary.password)
                            {
                                Message();
                                //  secretary.PrintMenu();
                                break;
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again:");
                        break;
                }
            }
        }

        public static void Message()
        {
            Console.WriteLine("Uspesno ste se ulogovali");
        }
    }
}






            
