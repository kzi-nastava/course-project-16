using System.Collections.Generic;

namespace Usi_Projekat
{
    public class RetiringRoom : HospitalRoom
    {
        public RetiringRoom(string id, string name) : base(id, name)
        {
            Furniture = new Dictionary<Furniture, int>();
        }
        public RetiringRoom(string id, string name, Dictionary<Furniture, int> furnitures) : base(id, name, furnitures)
        {}

    }
}