using System.Collections.Generic;
using Usi_Project.Users;

namespace Usi_Project.Repository.RoomRepository
{
    public interface IRoomRepository
    { 
        OperatingRoom GetOperatingRoomById(string id);
        OverviewRoom GetOverviewRoomById(string id);
        RetiringRoom GetRetiringRoomById(string id);
        void AddOperatingRoom(OperatingRoom room);
        void AddOverviewRoom(OverviewRoom room);
        void AddRetiringRoom(RetiringRoom room);
        void DeleteOperatingRoom(OperatingRoom room);
        void DeleteOverviewRoom(OverviewRoom room);
        void DeleteRetiringRoom(RetiringRoom room);
        void LoadData();
        void SaveData();
    }
}