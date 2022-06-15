using System;
using System.Collections.Generic;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class HospitalBrowser
    {

        public static void SearchHospitalEquipments(RoomRepository repository)
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
            Search(repository, eq, options, option2);
        }
        
        private static void Search(RoomRepository repository, string eq, List<int> options1, int option2)
        {
            foreach (var q in options1)
            {
                if (q == 1)
                    SearchThroughOperatingRooms(repository, eq.ToLower(), option2);
                else if (q == 2)
                    SearchThroughOverviewRooms(repository, eq.ToLower(), option2);
                else if (q == 3)
                    SearchThroughRetiringRooms(repository, eq.ToLower(), option2);
                else if (q == 4)
                    SearchThroughStockRoom(repository, eq.ToLower(), option2);
            }
        }

        private static void SearchThroughOperatingRooms(RoomRepository repository, string eq, int option)
        {
            foreach (OperatingRoom op in repository.OperatingRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");

                op.PrintSurgeryEquipments(eq, option);
                op.PrintFurniture(eq, option);

            }
        }

        private static void SearchThroughOverviewRooms(RoomRepository repository, string eq, int option)
        {
            foreach (OverviewRoom op in repository.OverviewRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintMedicalEquipments(eq, option);
                op.PrintFurniture(eq, option);
            }
        }

        private static void SearchThroughRetiringRooms(RoomRepository repository, string eq, int option)
        {
            foreach (RetiringRoom op in repository.RetiringRooms)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(op.Name);
                Console.WriteLine("---------------------");
                op.PrintFurniture(eq, option);
            }
        }
        

        private static void SearchThroughStockRoom(RoomRepository repository, string eq, int option)
        {
            repository.StockRoom.PrintMedicalEquipment(eq, option);
            repository.StockRoom.PrintSurgeryEquipment(eq, option);
            repository.StockRoom.PrintFurniture(eq, option);
        }
    }
}