using System;

namespace Usi_Project.Repository
{
    public class DrugViewer
    {
        public static string PrintMenu()
        {
            while (true)
            { 
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Adding new drugs to the system");
                Console.WriteLine("2) - View drugs");
                Console.WriteLine("3) - View rejected drugs");
                Console.WriteLine("x) - Back");
                Console.Write(">> ");
                string option = Console.ReadLine();
                option ??= "x";
                Console.WriteLine();
                return option;
            }
        }
    }
}