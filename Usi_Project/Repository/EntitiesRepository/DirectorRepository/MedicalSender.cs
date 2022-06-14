namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MedicalSender
    {

        public static void SendSurgeryEquipmentToStockRoom(Factory factory, OperatingRoom operatingRoom)
        {
            foreach (var tool in operatingRoom.SurgeryEquipments)
            {
                if (!factory.RoomRepository.StockRoom.SurgeryEquipment.ContainsKey(tool.Key))
                {
                    factory.RoomRepository.StockRoom.SurgeryEquipment[tool.Key] = tool.Value;
                }

                factory.RoomRepository.StockRoom.SurgeryEquipment[tool.Key] += tool.Value;
            }
        }

        public static void SendMedicalEquipmentToStockRoom(Factory factory, OverviewRoom overviewRoom)
        {
            foreach (var tool in overviewRoom.Tools)
            {
                if (!factory.RoomRepository.StockRoom.MedicalEquipment.ContainsKey(tool.Key))
                {
                    factory.RoomRepository.StockRoom.MedicalEquipment[tool.Key] = tool.Value;
                }

                factory.RoomRepository.StockRoom.MedicalEquipment[tool.Key] += tool.Value;
            }
        }
    }
}