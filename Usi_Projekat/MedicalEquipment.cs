using System.Collections.Generic;
namespace Usi_Projekat
{
    public enum MedicalTool
    {
        Ekg,
        Stethoscope,
        Generator,
        Monitor,
        Thermometer,
        Xray
    }

    public class MedicalEquipment
    {
        private Dictionary<MedicalTool, int> _tools;
        public MedicalEquipment()
        {
            _tools = new Dictionary<MedicalTool, int>();
        }

        public Dictionary<MedicalTool, int> MedicalTools
        {
            get => _tools;
            set => _tools = value;
        }
    }
}