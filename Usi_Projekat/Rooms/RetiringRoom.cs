namespace Usi_Projekat
{
    public class RetiringRoom : HospitalRoom
    {
        public RetiringRoom(string id, string name, RoomFurniture furniture) : base(id, name, furniture)
        {}
        protected RetiringRoom() {}
    }
}