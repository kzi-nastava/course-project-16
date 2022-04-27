using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Newtonsoft.Json;
namespace Usi_Projekat
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = "./../../../users.json";
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            List<User> patients = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(fileName), json);
            while (true)
            {
                var ind = 0;
                Console.WriteLine("Enter email:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter role:");
                Console.WriteLine("1) - Director");
                Console.WriteLine("2) - Doctor");
                Console.WriteLine("3) - Patient");
                Console.WriteLine("4) - Secretary");
                string role = Console.ReadLine();
                switch (role)
                {
                    case "1":
                            foreach (var director in Utills.directors)
                            {
                                if (username == director.email && password == director.password)
                                {
                                    ind = 1;
                                    break;
                                }
                            }
                        break;
                        
                    case "2":
                            foreach (var doctor in doctors)
                            {
                                if (username == doctor.email && password == doctor.password)
                                {
                                    ind = 1;
                                    break;
                                }
                            }
                        break;

                    case "3":
                            foreach (var patient in patients)
                            {
                                if (username == patient.email && password == patient.password)
                                {
                                    ind = 1;
                                    break;
                                }
                            }
                        break;

                    case "4":
                            foreach (var secretary in secretaries)
                            {
                                if (username == secretary.email && password == secretary.password)
                                {
                                    ind = 1;
                                    break;
                                }
                            }
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again:");
                            break;
                        



                }
                foreach (var g in patients)
                {
                    if (username == g.email && password == g.password)
                    {
                        ind = 1;
                        break;
                    }
                }

                if (ind == 1)
                {
                    Console.WriteLine("Uspesno ste se ulogovali");
                    break;
                }
                else
                {
                    Console.WriteLine("Pokusajte ponovo");
                }
            }
        }
    }
}






            
