using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MergingRooms
    {
  
        public static void MergeRoomsOfSameType(Factory factory)
        {
            Console.WriteLine("1) Merge into new Operating room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine("3) Merge into new Retiring room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergeOperatingRooms(factory);
                    break;
                case 2:
                    MergeOverviewRooms(factory);
                    break;
                case 3:
                    MergeRetiringRooms(factory);
                    break;
            }
        }
        
        private static void MergeRetiringRooms(Factory _factory)
        {
            Console.WriteLine("\nChoose first overview room\n");
            RetiringRoom first = RoomsBrowser.FindRetiringRoom(_factory.RoomRepository.RetiringRooms);
            Console.WriteLine("\nChoose second overview room\n");
            RetiringRoom second = RoomsBrowser.FindRetiringRoom(_factory.RoomRepository.RetiringRooms);
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }

            if (RoomRenovation.IsBeingRenovated(first) || RoomRenovation.IsBeingRenovated(second))
                return;
            var timeForRenovation = MergingRoomsOfDiffType.GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new overview room\n");
            RetiringRoom newRetiringRoom = RoomsMaker.CreateRetiringRoom(_factory);
            newRetiringRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOverviewRooms(Factory _factory)
        {
            Console.WriteLine("\nChoose first overview room\n");
            OverviewRoom first = RoomsBrowser.FindOverviewRoom(_factory.RoomRepository.OverviewRooms);
            Console.WriteLine("\nChoose second overview room\n");
            OverviewRoom second = RoomsBrowser.FindOverviewRoom(_factory.RoomRepository.OverviewRooms);
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }

            if (RoomRenovation.IsBeingRenovated(first) || RoomRenovation.IsBeingRenovated(second))
                return;
            var timeForRenovation = MergingRoomsOfDiffType.GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new overview room\n");
            OverviewRoom newOverviewRoom = RoomsMaker.CreateOverviewRoom(_factory);
            newOverviewRoom.Tools = MergeEquipments.MergeMedicalEquipments(first, second);
            newOverviewRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOperatingRooms(Factory _factory)
        {

            Console.WriteLine("\nChoose first operating room\n");
            OperatingRoom first = RoomsBrowser.FindOperatingRoom(_factory.RoomRepository.OperatingRooms);
            Console.WriteLine("\nChoose second operating room\n");
            OperatingRoom second = RoomsBrowser.FindOperatingRoom(_factory.RoomRepository.OperatingRooms);
            if (first.Id == second.Id)
            {
                Console.WriteLine("Input two different rooms!");
                return;
            }

            if (RoomRenovation.IsBeingRenovated(first) || RoomRenovation.IsBeingRenovated(second))
                return;
            var timeForRenovation = MergingRoomsOfDiffType.GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(first, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(second, timeForRenovation.Item1, timeForRenovation.Item2);

            Console.WriteLine("\nCreating new operating room\n");
            OperatingRoom newOperatingRoom = RoomsMaker.CreateOperatingRoom(_factory);
            newOperatingRoom.SurgeryEquipments = MergeEquipments.MergeSurgeryEquipments(first, second);
            newOperatingRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void SetTimeForRenovation(HospitalRoom hospitalRoom, DateTime start, DateTime end)
        {

            bool isFree = ValidationService.CheckRoom(start, end, hospitalRoom.Id);
            if (isFree)
                hospitalRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    start, end);
        }
        
        
    }
}