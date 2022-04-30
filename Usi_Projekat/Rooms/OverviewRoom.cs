using System.Collections.Generic;

namespace Usi_Projekat

{
    public class OverviewRoom : HospitalRoom
    {
        private Dictionary<MedicalTool, int> _tools;

        public OverviewRoom()
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public OverviewRoom(string id, string name, Dictionary<Furniture, int> furniture) : base(id, name, furniture)
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public OverviewRoom(Dictionary<MedicalTool, int> tools)
        {
            _tools = tools;
        }

        public OverviewRoom(string id, string name, Dictionary<MedicalTool, int> tools,  Dictionary<Furniture, int> furniture) 
            : base(id, name, furniture)
        {
            _tools = tools;
        }

        public Dictionary<MedicalTool, int> Tools
        {
            get => _tools;
            set => _tools = value;
        }
    }
    
}