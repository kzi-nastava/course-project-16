using System;
using System.Collections.Generic;
using Usi_Project.roomRepository.EntitiesRepository.DirectorRepository;

namespace Usi_Project.Repository
{
    public static class RoomChanger
    {
        public static void ChangeRetiringRoom(RetiringRoom retiring)
        {
            if (!retiring.IsDateTimeOfRenovationDefault())
            {
                DirectorManager.CheckIfRenovationIsEnded();
                if (!retiring.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + retiring.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.WriteLine("Enter new name:  ");
                        string newName = Console.ReadLine();
                        retiring.Name = newName;
                        break;
                    }
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public static void ChangeOvRoom(RoomRepository _manager, OverviewRoom overview, TimerManager timerManager)
        {
            if (!overview.IsDateTimeOfRenovationDefault())
            {
                DirectorManager.CheckIfRenovationIsEnded();
                if (!overview.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + overview.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.WriteLine("3) Change Medical Equipments");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.WriteLine("Enter new name: >> ");
                        string newName = Console.ReadLine();
                        overview.Name = newName;
                        break;
                    }
                    case "2":
                        FurnitureChanger.ChangeFurnitureInOvRoom(_manager, overview, timerManager);
                        break;
                        
                    case "3":
                        EquipmentChanger.ChangeMedicalTools(_manager, overview, timerManager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
         public static void ChangeOpRoom(RoomRepository repository, OperatingRoom operatingRoom, TimerManager timerManager)
        {
            if (!operatingRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + operatingRoom.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {

                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Change Name: ");
                Console.WriteLine("2) Change Furniture");
                Console.WriteLine("3) Change Surgery Equipments");
                Console.Write(">> ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                    {
                        Console.Write("Enter new name: >> ");
                        string newName = Console.ReadLine();
                        operatingRoom.Name = newName;
                        break;
                    }
                    case "2":
                        FurnitureChanger.ChangeFurnitureInOpRoom(repository, operatingRoom, timerManager);
                        break;
                    case "3":
                        EquipmentChanger.ChangeSurgeryTools(repository, operatingRoom, timerManager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
        public static DateTime GetTime()
        {
            Console.WriteLine("Input year >> ");
            int year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input month >> ");
            int month = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input day >> ");
            int day= Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input hour >> ");
            int hour = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input minute >> ");
            int minute = Int32.Parse(Console.ReadLine());
            return new DateTime(year, month, day, hour, minute, 0);
        }
        
    }
}