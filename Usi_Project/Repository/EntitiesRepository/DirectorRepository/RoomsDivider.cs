using System;
using System.Collections.Generic;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public static class SplitRooms
    {
        
 
         public  static  void SplitOperatingRoom(RoomRepository factory, OperatingRoom room, RoomFinder roomFinder)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + room.TimeOfRenovation.Value + "\n");
                return;
            }

            var timeForRenovationRoom = RoomRenovation.CheckTimeForRenovationRoom(room.Id);
            if (timeForRenovationRoom.Item3)
                room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);

            Console.WriteLine("Create first operating room");
            OperatingRoom firstRoom = (OperatingRoom) roomFinder.CreateRoom(typeof(OperatingRoom));
            Console.WriteLine("Create second operating room");

            OperatingRoom secondRoom = (OperatingRoom) roomFinder.CreateRoom(typeof(OperatingRoom));
            foreach (var equipment in room.SurgeryEquipments)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + room.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + room.Name + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.SurgeryEquipments[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.SurgeryEquipments[equipment.Key] = equipment.Value;
                        break;
                }

            }

            MergeEquipments.SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
            firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
            secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);

            factory.OperatingRooms.Remove(room);

        }
         
         public static void SplitOverwievRoom(RoomRepository factory, OverviewRoom room, RoomFinder roomFinder)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                  + room.TimeOfRenovation.Value + "\n");
                return;
            }

            var timeForRenovationRoom = RoomRenovation.CheckTimeForRenovationRoom(room.Id);
            if (timeForRenovationRoom.Item3)
                room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);

            Console.WriteLine("Create first overview room");
            OverviewRoom firstRoom = (OverviewRoom)  roomFinder.CreateRoom(typeof(OverviewRoom));
            Console.WriteLine("Create second operating room");
            OverviewRoom secondRoom = (OverviewRoom)  roomFinder.CreateRoom(typeof(OverviewRoom));
            foreach (var equipment in room.Tools)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + room.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + room.Name + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.Tools[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.Tools[equipment.Key] = equipment.Value;
                        break;
                }

            }

            MergeEquipments.SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
            firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
            secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
            factory.OverviewRooms.Remove(room);

        }
            
        public static void SplitRetiringRoom(RoomRepository factory, RetiringRoom room, RoomFinder roomFinder)
            {
                if (!room.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("\nYou can not split room, because room is being renovated until  "
                                      + room.TimeOfRenovation.Value + "\n");
                    return;
                }

                var timeForRenovationRoom = RoomRenovation.CheckTimeForRenovationRoom(room.Id);
                if (timeForRenovationRoom.Item3)
                    room.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                        timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);


                RetiringRoom firstRoom = (RetiringRoom) roomFinder.CreateRoom(typeof(RoomRepository));
                RetiringRoom secondRoom = (RetiringRoom) roomFinder.CreateRoom(typeof(RoomRepository));

                firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);

                MergeEquipments.SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
                factory.RetiringRooms.Remove(room);

            }

    }
}