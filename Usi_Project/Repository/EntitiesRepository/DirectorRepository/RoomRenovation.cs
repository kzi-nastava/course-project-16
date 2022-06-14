using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public  class RoomRenovation

    {


        public static void Renovation(Factory _factory)
        {
            DirectorMenus.PrintRoomsMenu();
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                   OperatingRoom operatingRoom = RoomsBrowser.FindOperatingRoom(_factory.RoomManager.OperatingRooms);
                    var timeForRenovationRoom = CheckTimeForRenovationRoom(operatingRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        operatingRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
                case 2:
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(_factory.RoomManager.OverviewRooms);
                    timeForRenovationRoom = CheckTimeForRenovationRoom(overviewRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        overviewRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
                case 3:
                    RetiringRoom  retiringRoom = RoomsBrowser.FindRetiringRoom(_factory.RoomManager.RetiringRooms);
                    timeForRenovationRoom = CheckTimeForRenovationRoom(retiringRoom.Id);
                    if (timeForRenovationRoom.Item3)
                        retiringRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                            timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                    break;
            }
        }
        
        public static void MultipleRoomRenovation(Factory _factory)
        {
            Console.WriteLine("Choose one of the renovation options below: ");
            Console.WriteLine("1) Dividing the room into two smaller rooms");
            Console.WriteLine("2) Merging two rooms into one larger room");
            Console.WriteLine(">> ");
            int choise = Int32.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    ChooseRoomForDividing(_factory);
                    break;
                case 2:
                    ChooseRoomsForMerging(_factory);
                    break;
            }
        }
        
        private static void ChooseRoomsForMerging(Factory _factory)
        {
            DirectorMenus.PrintMenuForMergingRooms();
            int choiseRoom = Int32.Parse(Console.ReadLine());
            switch (choiseRoom)
            {
                case 1:
                    MergingRooms.MergeRoomsOfSameType(_factory);
                    break;
                case 2:
                    Console.WriteLine("\nChoose operating room\n");
                   OperatingRoom operatingRoom = RoomsBrowser.FindOperatingRoom(_factory.RoomManager.OperatingRooms);
                    Console.WriteLine("\nChoose  retiring room\n");
                    RetiringRoom retiringRoom = RoomsBrowser.FindRetiringRoom(_factory.RoomManager.RetiringRooms);
                    if (IsBeingRenovated(operatingRoom) || IsBeingRenovated(retiringRoom))
                        return;
                    MergingRoomsOfDiffType.MergeOperatingAndRetiringRoom(_factory, operatingRoom, retiringRoom);
                    break;
                case 4:
                    Console.WriteLine("\nChoose  retiring room\n");
                    retiringRoom = RoomsBrowser.FindRetiringRoom(_factory.RoomManager.RetiringRooms);
                    Console.WriteLine("\nChoose overview room\n");
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(_factory.RoomManager.OverviewRooms);
                    MergingRoomsOfDiffType.MergeRetiringRoomAndOverviewRoom(_factory, retiringRoom, overviewRoom);
                    break;
                case 3:
                    Console.WriteLine("\nChoose  operating room\n");
                    operatingRoom = RoomsBrowser.FindOperatingRoom(_factory.RoomManager.OperatingRooms);
                    Console.WriteLine("\nChoose overview room\n");
                    overviewRoom = RoomsBrowser.FindOverviewRoom(_factory.RoomManager.OverviewRooms);
                    MergingRoomsOfDiffType.MergeOperatingAndOverviewRoom(_factory, operatingRoom, overviewRoom);
                    break;
                }
            }
        
        private static void ChooseRoomForDividing(Factory _factory)

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
                    OperatingRoom room = RoomsBrowser.FindOperatingRoom(_factory.RoomManager.OperatingRooms);
                    SplitRooms.SplitOperatingRoom(_factory, room);
                    break;
                case 2:
                    OverviewRoom overviewRoom = RoomsBrowser.FindOverviewRoom(_factory.RoomManager.OverviewRooms);
                    SplitRooms.SplitOverwievRoom(_factory,overviewRoom);
                    break;
                case 3:
                    RetiringRoom retiringRoom = RoomsBrowser.FindRetiringRoom(_factory.RoomManager.RetiringRooms);
                    SplitRooms.SplitRetiringRoom(_factory, retiringRoom);
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