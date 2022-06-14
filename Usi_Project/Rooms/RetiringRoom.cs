using System.Collections.Generic;
using System;
namespace Usi_Project
{
    public class RetiringRoom : HospitalRoom
    {
        public RetiringRoom()
        {
            Furniture = new Dictionary<Furniture, int>();
        }
        public RetiringRoom(string id, string name) : base(id, name)
        {
            Furniture = new Dictionary<Furniture, int>();
        }
        public RetiringRoom(string id, string name, Dictionary<Furniture, int> furnitures) : base(id, name, furnitures)
        {}
    }

    }
