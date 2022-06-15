namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class MedicalSender
    {

        public static void SendSurgeryEquipmentToStockRoom(RoomRepository repository, OperatingRoom operatingRoom)
        {
            foreach (var tool in operatingRoom.SurgeryEquipments)
            {
                if (!repository.StockRoom.SurgeryEquipment.ContainsKey(tool.Key))
                {
                    repository.StockRoom.SurgeryEquipment[tool.Key] = tool.Value;
                }

                repository.StockRoom.SurgeryEquipment[tool.Key] += tool.Value;
            }
        }

        public static void SendMedicalEquipmentToStockRoom(RoomRepository repository, OverviewRoom overviewRoom)
        {
            foreach (var tool in overviewRoom.Tools)
            {
                if (!repository.StockRoom.MedicalEquipment.ContainsKey(tool.Key))
                {
                    repository.StockRoom.MedicalEquipment[tool.Key] = tool.Value;
                }

                repository.StockRoom.MedicalEquipment[tool.Key] += tool.Value;
            }
        }
    }
}