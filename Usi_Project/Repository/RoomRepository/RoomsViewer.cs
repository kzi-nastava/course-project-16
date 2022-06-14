using System;

namespace Usi_Project.Repository
{
    public class RoomsViewer
    {
        public  static void ViewOverviewRooms(Factory _manager, OverviewRoom room)
        {
            if (!room.IsDateTimeOfRenovationDefault())
            {
                Console.WriteLine("The room is being renovated until  " + room.TimeOfRenovation.Value);
                return;
            }
            while (true)
            {
                room.PrintRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager.OverviewRooms.Remove(room);
                        break;
                    case "2":
                        RoomChanger.ChangeOvRoom(_manager, room);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public static  void ViewOperatingRooms(Factory _manager, OperatingRoom operatingRoom)
        {
            if (!operatingRoom.IsDateTimeOfRenovationDefault())
            {
                _manager.DirectorManager.CheckIfRenovationIsEnded();
                if (!operatingRoom.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + operatingRoom.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                operatingRoom.PrintRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager.OperatingRooms.Remove(operatingRoom);
                        break;
                    case "2":
                        RoomChanger.ChangeOpRoom(_manager, operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
        public static void ViewRetiringRoom(Factory _manager, RetiringRoom room)
        {
   
            if (!room.IsDateTimeOfRenovationDefault())
            {
                _manager.DirectorManager.CheckIfRenovationIsEnded();
                if (!room.IsDateTimeOfRenovationDefault())
                {
                    Console.WriteLine("The room is being renovated until  " + room.TimeOfRenovation.Value);
                    return;
                }
            }
            while (true)
            {
                room.PrintRoom();
                switch (GetOption())
                {
                    case "1":
                        _manager.RoomManager.RetiringRooms.Remove(room);
                        break;
                    case "2":
                        RoomChanger.ChangeRetiringRoom(_manager, room);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        

        private static string GetOption()
        {
            Console.WriteLine("Choose option or any for exit: ");
            Console.WriteLine("1) Delete room");
            Console.WriteLine("2) Change room");
            Console.Write(">> ");
            var option = Console.ReadLine();
            return option;

        }

    }
}