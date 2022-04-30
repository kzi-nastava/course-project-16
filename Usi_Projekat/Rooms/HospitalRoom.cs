using System.Collections.Generic;

namespace Usi_Projekat
{
    public class HospitalRoom
    {
        private string _id;
        private string _name;
        private Dictionary<Furniture, int> _furniture;
        
        public HospitalRoom()
        {
            _id = "";
            _name = "";
            _furniture = new Dictionary<Furniture, int>();
            
        }
        public Dictionary<Furniture, int> Furniture
        {
            get => _furniture;
            set => _furniture = value;
        }

        public HospitalRoom(string id, string name, Dictionary<Furniture, int> furniture)
        {
            _id = id;
            _name = name;
            _furniture = furniture;
        }

        public HospitalRoom(string id, string name)
        {
            _id = id;
            _name = name;
            _furniture = new Dictionary<Furniture, int>();
        }

        public string Id
        {
            get => _id;
            set =>_id = value;
        }
        
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
    }
}