using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MergingRooms
    {

        private static RoomFinder _roomFinder;

        public MergingRooms(RoomFinder roomFinder)
        {
            _roomFinder = roomFinder;
        }
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
            RetiringRoom first = RoomFinder.FindRetiringRoom(repository.RetiringRooms);
            Console.WriteLine("\nChoose second overview room\n");
            RetiringRoom second = RoomFinder.FindRetiringRoom(repository.RetiringRooms);
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
            RetiringRoom newRetiringRoom = (RetiringRoom)_roomFinder.CreateRoom(typeof(RoomRepository));
            newRetiringRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOverviewRooms(RoomRepository repository)
        {
            Console.WriteLine("\nChoose first overview room\n");
            OverviewRoom first = RoomFinder.FindOverviewRoom(repository.OverviewRooms);
            Console.WriteLine("\nChoose second overview room\n");
            OverviewRoom second = RoomFinder.FindOverviewRoom(repository.OverviewRooms);
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
            OverviewRoom newOverviewRoom = (OverviewRoom) _roomFinder.CreateRoom(typeof(OverviewRoom));
            newOverviewRoom.Tools = MergeEquipments.MergeMedicalEquipments(first, second);
            newOverviewRoom.Furniture = MergeEquipments.MergeFurniture(first, second);
            SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            first.ForRemove = true;
            second.ForRemove = true;

        }
        
        private static void MergeOperatingRooms(RoomRepository repository)
        {

            Console.WriteLine("\nChoose first operating room\n");
            OperatingRoom first = RoomFinder.FindOperatingRoom(repository.OperatingRooms);
            Console.WriteLine("\nChoose second operating room\n");
            OperatingRoom second = RoomFinder.FindOperatingRoom(repository.OperatingRooms);
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
            OperatingRoom newOperatingRoom = (OperatingRoom)_roomFinder.CreateRoom(typeof(OperatingRoom));
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