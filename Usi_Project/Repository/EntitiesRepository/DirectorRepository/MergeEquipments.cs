using System;
using System.Collections.Generic;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MergeEquipments
    {
        public static Dictionary<SurgeryTool, int> MergeSurgeryEquipments(OperatingRoom first, OperatingRoom second)
        {
            Dictionary<SurgeryTool, int> ret = new Dictionary<SurgeryTool, int>();
            foreach (var tool in first.SurgeryEquipments)
            {
                ret[tool.Key] = tool.Value;
            }

            foreach (var tool in second.SurgeryEquipments)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;
        }
        
        public static Dictionary<MedicalTool, int> MergeMedicalEquipments(OverviewRoom first, OverviewRoom second)
        {
            Dictionary<MedicalTool, int> ret = new Dictionary<MedicalTool, int>();
            foreach (var tool in first.Tools)
            {
                ret[tool.Key] = tool.Value;
            }

            foreach (var tool in second.Tools)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;

        }
        
        public static Dictionary<Furniture, int> MergeFurniture(HospitalRoom hospitalRoom1, HospitalRoom hospitalRoom2)
        {
            Dictionary<Furniture, int> ret = new Dictionary<Furniture, int>();
            foreach (var tool in hospitalRoom1.Furniture)
            {
                ret[tool.Key] = tool.Value;
            }

            foreach (var tool in hospitalRoom2.Furniture)
            {
                if (!ret.ContainsKey(tool.Key))
                    ret[tool.Key] = tool.Value;
                ret[tool.Key] += tool.Value;
            }

            return ret;

        }
        
        public static void SplitFurnitureThroughRooms(HospitalRoom parentRoom, HospitalRoom firstRoom,
            HospitalRoom secondRoom)
        {
            foreach (var equipment in parentRoom.Furniture)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) Add  " + equipment + " from " + firstRoom.Name + "  to " + firstRoom.Name);
                Console.WriteLine("2) Add  " + equipment + " from " + secondRoom.Name + " to " + secondRoom.Name);
                Console.WriteLine(">> ");
                int choise = Int32.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        firstRoom.Furniture[equipment.Key] = equipment.Value;
                        break;
                    case 2:
                        secondRoom.Furniture[equipment.Key] = equipment.Value;
                        break;
                }
            }
        }
    }
}