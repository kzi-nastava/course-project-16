using System;

namespace Usi_Project.Repository
{
    public class InputRoomsInfo
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
    }
}