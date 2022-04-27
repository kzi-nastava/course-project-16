using System.Collections.Generic;

namespace Usi_Projekat
{
    public enum SurgeryTool
    {
        Defibrillator,
        Clamps,
        Scissors,
        Scalpels,
        CottonWool,
        Table,
        Lamp
    }
    public class SurgeryEquipment
    {
        private Dictionary<SurgeryTool, int> _tools;

        public SurgeryEquipment()
        {
            _tools = new Dictionary<SurgeryTool, int>();
        }

        public Dictionary<SurgeryTool, int> Tools
        {
            get => _tools;
            set => _tools = value;
        }
    }


}