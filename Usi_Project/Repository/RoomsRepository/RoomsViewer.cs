using System;
using Usi_Project.Repository.EntitiesRepository.DirectorsRepository;

namespace Usi_Project.Repository
{
    public static class RoomsViewer
    {
        public  static void ViewOverviewRooms(RoomRepository _repository, OverviewRoom room, TimerManager _manager=null)
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
                        _repository.OverviewRooms.Remove(room);
                        break;
                    case "2":
                        RoomService.ChangeOvRoom(_repository, room, _manager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public static  void ViewOperatingRooms(RoomRepository _repository, OperatingRoom operatingRoom, TimerManager _manager)
        {
            if (!operatingRoom.IsDateTimeOfRenovationDefault())
            {
                DirectorService.CheckIfRenovationIsEnded();
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
                        _repository.OperatingRooms.Remove(operatingRoom);
                        break;
                    case "2":
                        RoomService.ChangeOpRoom( _repository, operatingRoom, _manager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        
        public static void ViewRetiringRoom(RoomRepository _repository, RetiringRoom room)
        {
   
            if (!room.IsDateTimeOfRenovationDefault())
            {
                DirectorService.CheckIfRenovationIsEnded();
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
                        _repository.RetiringRooms.Remove(room);
                        break;
                    case "2":
                        RoomService.ChangeRetiringRoom(room);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        public static void ViewStockRoom(RoomRepository repository)
        {
            repository.StockRoom.PrintRoom();
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