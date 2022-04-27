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
            string fileName = "/Users/bane/Desktop/prazan/Usi_Projekat/users.json";
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
             List<User> patients = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(fileName), json);
             foreach (var g in patients)
                 Console.WriteLine(g.email);
             {
                 
             }
        }
    }
}