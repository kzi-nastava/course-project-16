using System;
using System.Collections.Generic;

namespace Usi_Project.Repository
{
    public static class InputRoomsInfo
    {
        public static string GetInfoForCreatingNewRoom()
        {
            Console.WriteLine("Input id of new Overview Room >> ");
            string id = Console.ReadLine();
            return id;
        }

        public static void SetWarningThatIdExists(string id)
        {
            Console.WriteLine("ID " + id + " already exists. Try again");
        }

        public static string GetNameOfNewRoom()
        {
            Console.WriteLine("Input name of new Overview room >> ");
            string name = Console.ReadLine();
            return name;
        }

        public static string GetOptionForChangingFurniture()
        {
            Console.WriteLine("Choose option or x for exit: ");
            Console.WriteLine("1) Add new furniture");
            Console.WriteLine("2) Remove current furniture");
            Console.Write(">> ");
            return Console.ReadLine();
        }
        
    }
}