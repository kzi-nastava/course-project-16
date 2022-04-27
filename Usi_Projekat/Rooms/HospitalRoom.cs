namespace Usi_Projekat
{
    public class HospitalRoom
    {
        private readonly string _id;
        private string _name;
        private RoomFurniture _furniture;


        public HospitalRoom(string id, string name, RoomFurniture furniture)
        {
            _id = id;
            _name = name;
            _furniture = furniture;
        }

        public RoomFurniture Furniture
        {
            get => _furniture;
            set => _furniture = value;
        }

        public string Id => _id;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        protected HospitalRoom()
        {
            _furniture = new RoomFurniture();
        }
        protected HospitalRoom(string id, string name)
        {
            _id = id;
            _name = name;
            _furniture = new RoomFurniture();
        }
    }
}