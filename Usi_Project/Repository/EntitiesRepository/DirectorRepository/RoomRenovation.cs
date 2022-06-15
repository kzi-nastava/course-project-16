using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public  class RoomRenovation

    {


        public static void Renovation(RoomRepository repository)
        {
            DirectorMenus.PrintRoomsMenu();
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                   OperatingRoom operatingRoom = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
                    var timeForRenovationRoom = CheckTimeForRenovationRoom(operatingRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
                case 2:
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(repository.OverviewRooms);
                    timeForRenovationRoom = CheckTimeForRenovationRoom(overviewRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
                case 3:
                    RetiringRoom  retiringRoom = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
                    timeForRenovationRoom = CheckTimeForRenovationRoom(retiringRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        retiringRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
            }
        }
        
        public static void MultipleRoomRenovation(RoomRepository repository)
        {
            Console.WriteLine("Choose one of the renovation options below: ");
            Console.WriteLine("1) Dividing the room into two smaller rooms");
            Console.WriteLine("2) Merging two rooms into one larger room");
            Console.WriteLine(">> ");
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    ChooseRoomForDividing(repository);
                    break;
                case 2:
                    ChooseRoomsForMerging(repository);
                    break;
            }
        }
        
        private static void ChooseRoomsForMerging(RoomRepository repository)
        {
            DirectorMenus.PrintMenuForMergingRooms();
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergingRooms.MergeRoomsOfSameType(repository);
                    break;
                case 2:
                    Console.WriteLine("\nChoose operating room\n");
                   OperatingRoom operatingRoom = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
                    Console.WriteLine("\nChoose  retiring room\n");
                    RetiringRoom retiringRoom = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
                    if (IsBeingRenovated(operatingRoom) || IsBeingRenovated(retiringRoom))
                        return;
                    MergingRoomsOfDiffType.MergeOperatingAndRetiringRoom(repository, operatingRoom, retiringRoom);
                    break;
                case 4:
                    Console.WriteLine("\nChoose  retiring room\n");
                    retiringRoom = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
                    Console.WriteLine("\nChoose overview room\n");
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(repository.OverviewRooms);
                    MergingRoomsOfDiffType.MergeRetiringRoomAndOverviewRoom(repository, retiringRoom, overviewRoom);
                    break;
                case 3:
                    Console.WriteLine("\nChoose  operating room\n");
                    operatingRoom = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
                    Console.WriteLine("\nChoose overview room\n");
                    overviewRoom = RoomsBrowser.FindOverviewRoom(repository .OverviewRooms);
                    MergingRoomsOfDiffType.MergeOperatingAndOverviewRoom(repository, operatingRoom, overviewRoom);
                    break;
                }
            }
        
        private static void ChooseRoomForDividing(RoomRepository repository)

        {
            Console.WriteLine("Choose type of hospital room: ");
            Console.WriteLine("1) Operating room");
            Console.WriteLine("2) Overview room");
            Console.WriteLine("3) Retiring room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    OperatingRoom room = RoomsBrowser.FindOperatingRoom(repository.OperatingRooms);
                    SplitRooms.SplitOperatingRoom(repository, room);
                    break;
                case 2:
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(repository.OverviewRooms);
                    SplitRooms.SplitOverwievRoom(repository, overviewRoom);
                    break;
                case 3:
                    RetiringRoom retiringRoom = RoomsBrowser.FindRetiringRoom(repository.RetiringRooms);
                    SplitRooms.SplitRetiringRoom(repository, retiringRoom);
                    break;
            }
        }
        
        
        public static (DateTime, DateTime, bool) CheckTimeForRenovationRoom(string idRoom)
        {
            Console.WriteLine("Input time for start room renovation");
            DateTime timeStart = ScheduleService.CreateDate();
            Console.WriteLine("Input end time for room renovation");
            DateTime timeEnd = ScheduleService.CreateDate();
            bool isFree = ValidationService.CheckRoom(timeStart,
                timeEnd, idRoom);
            return (timeStart, timeEnd, isFree);
        }
        
        public static bool IsBeingRenovated(HospitalRoom hospitalRoom)
        {
            if (!hospitalRoom.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + hospitalRoom.TimeOfRenovation.Value + "\n");
                return true;
            }
        
            return false;
        }
    }
}