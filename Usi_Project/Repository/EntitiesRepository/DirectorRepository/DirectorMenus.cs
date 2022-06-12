using System;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public static class DirectorMenus
    {
        public static void PrintMainMenu()
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - View hospital rooms");
            Console.WriteLine("2) - Create hospital rooms");
            Console.WriteLine("3) - Search hospital equipments");
            Console.WriteLine("4) - Refresh changes");
            Console.WriteLine("5) - Scheduling room renovation");
            Console.WriteLine("6) - Scheduling multiple room renovation");
            Console.WriteLine("7) - Drug and ingredient management");
            Console.WriteLine("x) - Exit.");
            Console.WriteLine(">> ");
        }
        
        public static void PrintRoomsMenu()
        {
            
            Console.WriteLine("Choose type of hospital room: ");
            Console.WriteLine("1) Operating room");
            Console.WriteLine("2) Overview room");
            Console.WriteLine("3) Retiring room");
            Console.WriteLine("4) Stock room");
            Console.WriteLine("5) Ok");
            Console.WriteLine(">> ");
        }
        
        public static void PrintQuantityMenu()
        {
            Console.WriteLine("Choose quantity of equipment: ");
            Console.WriteLine("1) Out of stock");
            Console.WriteLine("2) 0-10");
            Console.WriteLine("3) 10+");
            Console.WriteLine("4) Ok");
            Console.WriteLine(">> ");
        }
        
        public static void PrintMenuForMergingRooms()
        {
            Console.WriteLine("Choose type of hospital room for merging: ");
            Console.WriteLine("1) Merge rooms of same type");
            Console.WriteLine("2) Merge operating room with retiring room");
            Console.WriteLine("3) Merge operating room with overview room");
            Console.WriteLine("4) Merge overview room with retiring room");
            Console.WriteLine(">> ");
        }

        public static void PrintCreatingRoomsOptions()
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create Overview room");
            Console.WriteLine("2) - Create Operating room");
            Console.WriteLine("3) - Create Retiring room");
            Console.WriteLine("x) - Back");
            Console.Write(">> ");
        }

        public static void PrintViewRoomsOptions()
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1. View Operating rooms");
            Console.WriteLine("2. View Overview rooms");
            Console.WriteLine("3. View Retiring rooms");
            Console.WriteLine("3. View Stock room");
            Console.Write(">> ");
        }
    }
}