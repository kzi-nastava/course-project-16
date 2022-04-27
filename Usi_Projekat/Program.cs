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
                Console.WriteLine("Enter email");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password");
                string password = Console.ReadLine();
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






            
