using System.Collections.Generic;
using System;
namespace Usi_Project
{
    public class RetiringRoom : HospitalRoom
    {
        public RetiringRoom()
        {
            Furniture = new Dictionary<Furniture, int>();
        }
        public RetiringRoom(string id, string name) : base(id, name)
        {
            Furniture = new Dictionary<Furniture, int>();
        }
        public RetiringRoom(string id, string name, Dictionary<Furniture, int> furnitures) : base(id, name, furnitures)
        {}
        
        public void printRoom()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("ID: " + Id); 
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Furniture:");
            Console.WriteLine("-------------------------");
            foreach (var tools in Furniture)
                Console.WriteLine("\t" + tools.Key + ": " + tools.Value);
            Console.WriteLine("=========================");
            
        }
  
    }

    }
