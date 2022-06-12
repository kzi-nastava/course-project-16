using System;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class RoomsMaker
    { 
        public static OverviewRoom CreateOverviewRoom(Factory factory)
        {
            while (true)
            {
                bool ind = false;
                Console.WriteLine("Input id of new Overview Room >> ");
                string id = Console.ReadLine();
                foreach (OverviewRoom overviewRoom in factory.RoomManager.OverviewRooms)
                {
                    if (overviewRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                        ind = true;
                        break;
                    }
                }

                if (ind) continue;
                Console.WriteLine("Input name of new Overview room >> ");
                string name = Console.ReadLine();
                OverviewRoom newRoom = new OverviewRoom(id, name);
                factory.RoomManager.OverviewRooms.Add(newRoom);
                return newRoom;
            }
        }
        
        public static OperatingRoom CreateOperatingRoom(Factory factory)
        {
            while (true)
            {
                Console.WriteLine("Input id of new Operating Room >> ");
                var id = Console.ReadLine();
                foreach (var operatingRoom in factory.RoomManager.OperatingRooms)
                {
                    if (operatingRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }

                Console.WriteLine("Input name of new Operating room >> ");
                var roomName = Console.ReadLine();
                OperatingRoom ovpRoom = new OperatingRoom(id, roomName);
                factory.RoomManager.OperatingRooms.Add(ovpRoom);
                return ovpRoom;
            }
        }
        
        public static RetiringRoom CreateRetiringRoom(Factory factory)
        {
            while (true)
            {
                Console.WriteLine("Input id of new Retiring Room >> ");
                string id = Console.ReadLine();
                foreach (RetiringRoom retiringRoom in factory.RoomManager.RetiringRooms)
                {
                    if (retiringRoom.Id == id)
                    {
                        Console.WriteLine("Id already exists. Try again");
                    }
                }

                Console.WriteLine("Input name of new Retiring room >> ");
                string roomName = Console.ReadLine();
                RetiringRoom newRetiringRoom = new RetiringRoom(id, roomName);
                factory.RoomManager.RetiringRooms.Add(newRetiringRoom);
                return newRetiringRoom;
            }
        }


    }
}