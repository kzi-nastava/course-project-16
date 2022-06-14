using System;
using System.Collections.Generic;

namespace Usi_Project
{
    public class DynamicRequest
    {
        private DateTime timeOfAddition;
        private Dictionary<DynamicEquipment, int> _dynamicTool;


        public DynamicRequest(DateTime timeOfAddition, Dictionary<DynamicEquipment, int> dynamicTool)
        {
            this.timeOfAddition = timeOfAddition;
            _dynamicTool = dynamicTool;
        }

        public DateTime TimeOfAddition
        {
            get => timeOfAddition;
            set => timeOfAddition = value;
        }

        public Dictionary<DynamicEquipment, int> DynamicEquipment
        {
            get => _dynamicTool;
            set => _dynamicTool = value;
        }
    }
}