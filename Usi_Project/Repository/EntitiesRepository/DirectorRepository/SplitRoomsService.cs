using System;
using System.Collections.Generic;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class SplitRooms
    {
 
         public  static  void SplitOperatingRoom(Factory factory, OperatingRoom room)
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
            OperatingRoom firstRoom = RoomsMaker.CreateOperatingRoom(factory);
            Console.WriteLine("Create second operating room");

            OperatingRoom secondRoom = RoomsMaker.CreateOperatingRoom(factory);
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

            factory.RoomRepository.OperatingRooms.Remove(room);

        }
         
         public static void SplitOverwievRoom(Factory factory, OverviewRoom room)
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
            OverviewRoom firstRoom = RoomsMaker.CreateOverviewRoom(factory);
            Console.WriteLine("Create second operating room");
            OverviewRoom secondRoom = RoomsMaker.CreateOverviewRoom(factory);
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
            factory.RoomRepository.OverviewRooms.Remove(room);

        }
            
        public static void SplitRetiringRoom(Factory factory, RetiringRoom room)
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


                RetiringRoom firstRoom = RoomsMaker.CreateRetiringRoom(factory);
                RetiringRoom secondRoom = RoomsMaker.CreateRetiringRoom(factory);

                firstRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);
                secondRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    timeForRenovationRoom.Item1, timeForRenovationRoom.Item2);

                MergeEquipments.SplitFurnitureThroughRooms(room, firstRoom, secondRoom);
                factory.RoomRepository.RetiringRooms.Remove(room);

            }

    }
}