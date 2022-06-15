using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MergingRooms
    {
  
        public static void MergeRoomsOfSameType(RoomRepository repository)
        {
            Console.WriteLine("1) Merge into new Operating room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine("3) Merge into new Retiring room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergeOperatingRooms(repository);
                    break;
                case 2:
                    MergeOverviewRooms(repository);
                    break;
                case 3:
                    MergeRetiringRooms(repository);
                    break;
            }
        }
        
        private static void MergeRetiringRooms(RoomRepository repository)
        {
            Console.WriteLine("\nChoose first overview room\n");
            RetiringRoom first = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
            Console.WriteLine("\nChoose second overview room\n");
            RetiringRoom second = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
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
            RetiringRoom newRetiringRoom = RoomsMaker.CreateRetiringRoom(repository);
            newRetiringRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOverviewRooms(RoomRepository repository)
        {
            Console.WriteLine("\nChoose first overview room\n");
            OverviewRoom first = RoomsBrowser.FindOverviewRoom(repository.OverviewRooms);
            Console.WriteLine("\nChoose second overview room\n");
            OverviewRoom second = RoomsBrowser.FindOverviewRoom(repository.OverviewRooms);
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
            OverviewRoom newOverviewRoom = RoomsMaker.CreateOverviewRoom(repository);
            newOverviewRoom.Tools = MergeEquipments.MergeMedicalEquipments(first, second);
            newOverviewRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOperatingRooms(RoomRepository repository)
        {

            Console.WriteLine("\nChoose first operating room\n");
            OperatingRoom first = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
            Console.WriteLine("\nChoose second operating room\n");
            OperatingRoom second = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
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
            OperatingRoom newOperatingRoom = RoomsMaker.CreateOperatingRoom(repository);
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