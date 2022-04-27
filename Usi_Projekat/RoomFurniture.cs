using System.Collections.Generic;

namespace Usi_Projekat
{
    public enum Furniture
    {
        Table,
        Chair,
        Bad,
        Closet
    }
    public class RoomFurniture
    {
        private Dictionary<Furniture, int> _furniture;
        public RoomFurniture()
        {
            _furniture = new Dictionary<Furniture, int>();
        }
        public RoomFurniture(Dictionary<Furniture, int> furniture)
        {
            _furniture = furniture;
        }

        public Dictionary<Furniture, int> Furniture
        {
            get => _furniture;
            set => _furniture = value;
        }
    }
}