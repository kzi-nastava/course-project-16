using System;
using System.Collections.Generic;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class HospitalBrowser
    {

        public static void SearchHospitalEquipments(Factory factory)
        {
            Console.WriteLine("Enter equipment: >> ");
            string eq = Console.ReadLine();
            List<int> options = new List<int>();
            while (!options.Contains(5))
            {
                DirectorMenus.PrintRoomsMenu();
                options.Add(Int32.Parse(Console.ReadLine()));
            }

            DirectorMenus.PrintQuantityMenu();
            int option2 = Int32.Parse(Console.ReadLine());
            Search(factory, eq, options, option2);
        }
        
        private static void Search(Factory factory, string eq, List<int> options1, int option2)
        {
            foreach (var q in options1)
            {
                if (q == 1)
                    SearchThroughOperatingRooms(factory, eq.ToLower(), option2);
                else if (q == 2)
                    SearchThroughOverviewRooms(factory, eq.ToLower(), option2);
                else if (q == 3)
                    SearchThroughRetiringRooms(factory, eq.ToLower(), option2);
                else if (q == 4)
                    SearchThroughStockRoom(factory, eq.ToLower(), option2);
            }
        }

        private static void SearchThroughOperatingRooms(Factory factory, string eq, int option)
        {
            foreach (OperatingRoom op in factory.RoomRepository.OperatingRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");

                op.PrintSurgeryEquipments(eq, option);
                op.PrintFurniture(eq, option);

            }
        }

        private static void SearchThroughOverviewRooms(Factory factory, string eq, int option)
        {
            foreach (OverviewRoom op in factory.RoomRepository.OverviewRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintMedicalEquipments(eq, option);
                op.PrintFurniture(eq, option);
            }
        }

        private static void SearchThroughRetiringRooms(Factory factory, string eq, int option)
        {
            foreach (RetiringRoom op in factory.RoomRepository.RetiringRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintFurniture(eq, option);
            }
        }
        

        private static void SearchThroughStockRoom(Factory factory, string eq, int option)
        {
            factory.RoomRepository.StockRoom.PrintMedicalEquipment(eq, option);
            factory.RoomRepository.StockRoom.PrintSurgeryEquipment(eq, option);
            factory.RoomRepository.StockRoom.PrintFurniture(eq, option);
        }
    }
}