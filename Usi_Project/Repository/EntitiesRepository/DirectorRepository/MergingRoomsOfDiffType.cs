using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MergingRoomsOfDiffType
    {
        public static void MergeOperatingAndOverviewRoom(Factory factory, OperatingRoom operatingRoom, OverviewRoom overviewRoom)
        {

            Console.WriteLine("1) Merge into new Operating room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(operatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(overviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            switch (choiseRoom)
            {
                case 1:
                    OperatingRoom newOperatingRoom = RoomsMaker.CreateOperatingRoom(factory);
                    newOperatingRoom.SurgeryEquipments = operatingRoom.SurgeryEquipments;
                    newOperatingRoom.Furniture = MergeEquipments.MergeFurniture(operatingRoom, overviewRoom);
                    MedicalSender.SendMedicalEquipmentToStockRoom(factory, overviewRoom);
                    Console.WriteLine("Medical Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
                case 2:
                    OverviewRoom newOverviewRoom = RoomsMaker.CreateOverviewRoom(factory);
                    newOverviewRoom.Tools = overviewRoom.Tools;
                    newOverviewRoom.Furniture = MergeEquipments.MergeFurniture(operatingRoom, overviewRoom);
                    MedicalSender.SendSurgeryEquipmentToStockRoom(factory, operatingRoom);
                    Console.WriteLine("Surgery Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }

            overviewRoom.ForRemove = true;
            operatingRoom.ForRemove = true;

        }
        
        public static void MergeRetiringRoomAndOverviewRoom(Factory factory, RetiringRoom retiringRoom, OverviewRoom overviewRoom)
        {

            Console.WriteLine("1) Merge into new Retiring room");
            Console.WriteLine("2) Merge into new Overview room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(retiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(overviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);

            switch (choiseRoom)
            {
                case 1:
                    RetiringRoom newRetiringRoom = RoomsMaker.CreateRetiringRoom(factory);
                    newRetiringRoom.Furniture = MergeEquipments.MergeFurniture(retiringRoom, overviewRoom);
                    MedicalSender.SendMedicalEquipmentToStockRoom(factory, overviewRoom);
                    Console.WriteLine("Medical Equipments from overview room send to Stock Room.");
                    SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);

                    break;
                case 2:
                    OverviewRoom newOverviewRoom = RoomsMaker.CreateOverviewRoom(factory);
                    newOverviewRoom.Tools = overviewRoom.Tools;
                    newOverviewRoom.Furniture = MergeEquipments.MergeFurniture(retiringRoom, overviewRoom);
                    ;
                    SetTimeForRenovation(newOverviewRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }

            overviewRoom.ForRemove = true;
            retiringRoom.ForRemove = true;

        }
        
        private static void SetTimeForRenovation(HospitalRoom hospitalRoom, DateTime start, DateTime end)
        {

            bool isFree = ValidationService.CheckRoom(start, end, hospitalRoom.Id);
            if (isFree)
                hospitalRoom.TimeOfRenovation = new KeyValuePair<DateTime, DateTime>(
                    start, end);
            
        }
        
        public  static void MergeOperatingAndRetiringRoom(Factory factory, OperatingRoom operatingRoom, RetiringRoom retiringRoom)
        {
            
            Console.WriteLine("1) Merge into new Retiring room");
            Console.WriteLine("2) Merge into new Operating room");
            Console.WriteLine(">> ");
            int choiseRoom = Int32.Parse(Console.ReadLine());
            var timeForRenovation = GetStartAndEndTimeForRenovation();
            SetTimeForRenovation(retiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            SetTimeForRenovation(operatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
            switch (choiseRoom)
            {
                case 1:
                    RetiringRoom newRetiringRoom = RoomsMaker.CreateRetiringRoom(factory);
                    newRetiringRoom.Furniture = MergeEquipments.MergeFurniture(retiringRoom, operatingRoom);
                    MedicalSender.SendSurgeryEquipmentToStockRoom(factory, operatingRoom);
                    Console.WriteLine("Surgery Equipments from operating room send to Stock Room.");
                    SetTimeForRenovation(newRetiringRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
                case 2:
                    OperatingRoom newOperatingRoom = RoomsMaker.CreateOperatingRoom(factory);
                    newOperatingRoom.Furniture = MergeEquipments.MergeFurniture(retiringRoom, operatingRoom);
                    newOperatingRoom.SurgeryEquipments = operatingRoom.SurgeryEquipments;
                    SetTimeForRenovation(newOperatingRoom, timeForRenovation.Item1, timeForRenovation.Item2);
                    break;
            }
            retiringRoom.ForRemove = true;
            operatingRoom.ForRemove = true;

        }

        
        public static (DateTime, DateTime) GetStartAndEndTimeForRenovation()
        {
            Console.WriteLine("Input time for start room renovation");
            DateTime timeStart = ScheduleService.CreateDate();
            Console.WriteLine("Input end time for room renovation");
            DateTime timeEnd = ScheduleService.CreateDate();
            return (timeStart, timeEnd);
        }


    }
}