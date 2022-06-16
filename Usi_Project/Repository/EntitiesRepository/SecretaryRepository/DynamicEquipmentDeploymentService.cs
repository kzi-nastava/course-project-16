using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Specialized;
using Usi_Project.Users;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository
{
    public class DynamicEquipmentDeploymentService
    {
        public List<string> PrintLowSupplyEquipmentFromAllRooms()
        {
            List<string> roomsId = new List<string>();
            foreach (OperatingRoom operatingRoom in SecretariesRepository._manager.RoomRepository.OperatingRooms)
            {
                roomsId =  operatingRoom.PrintLowSupplyEquipment(roomsId);
            }
            foreach (OverviewRoom overviewRoom in SecretariesRepository._manager.RoomRepository.OverviewRooms)
            {
                roomsId = overviewRoom.PrintLowSupplyEquipment(roomsId);
            }
            roomsId = SecretariesRepository._manager.RoomRepository.StockRoom.PrintLowSupplyEquipment(roomsId);
            return roomsId;
        }


        public string GetReceivingRoomid(List<string> roomsId)
        {
            string receivingRoomId = "";
            while (true)
            {
                Console.WriteLine("Enter id of the room that you want to add equipment to: ");
                receivingRoomId = Console.ReadLine();
                if (roomsId.Contains(receivingRoomId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid id entered, try again.");
                }
            }
            return receivingRoomId;
        }
        
        
        public string GetSupplyingRoomid(List<string> roomsId)
        {
            string supplyingRoomId = "";
            while (true)
            {
                Console.WriteLine("Enter id of the room that you want to take equipment from: ");
                supplyingRoomId = Console.ReadLine();
                if (roomsId.Contains(supplyingRoomId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid id entered, try again.");
                }
            }
            return supplyingRoomId;
        }

        public int ValidateInput()
        {
            int chosenDynamicEq;
            while (true)
            {
                chosenDynamicEq = Convert.ToInt32(Console.ReadLine());
                if (chosenDynamicEq > 4 || chosenDynamicEq < 0)
                {
                    Console.WriteLine("Wrong option entered, try again: ");
                }
                else
                {
                    break;
                }
            }

            return chosenDynamicEq;
        }
        public void DeploymentOfDynamicEquipment()
        {
            List<string> roomsId = PrintLowSupplyEquipmentFromAllRooms();
            string receivingRoomId = GetReceivingRoomid(roomsId);
            string supplyingRoomId = GetSupplyingRoomid(roomsId);
            Console.WriteLine("Enter Dynamic tool you want to move (x for exit): ");
            int numberOfTools;
            Dictionary<int, DynamicEquipment> dictionary = new Dictionary<int, DynamicEquipment>();
            int i = -1;
            foreach (DynamicEquipment eq in Enum.GetValues(typeof(DynamicEquipment)))
            {
                i++;
                dictionary[i] = eq;
                Console.WriteLine( i + ")" + " " + dictionary[i]);
            }
            int chosenDynamicEq = ValidateInput();
            DynamicEquipment chosen = dictionary[chosenDynamicEq];
            Console.WriteLine("Enter how much do you want to move: ");
            numberOfTools = Convert.ToInt32(Console.ReadLine());
            foreach (OverviewRoom receivingRoom in SecretariesRepository._manager.RoomRepository.OverviewRooms)
                GiveEqToOvRoom(receivingRoomId, numberOfTools, supplyingRoomId, receivingRoom, chosen);
            foreach (OperatingRoom receivingRoom in SecretariesRepository._manager.RoomRepository.OperatingRooms)
                GiveEqToOpRoom(receivingRoomId, numberOfTools, supplyingRoomId, receivingRoom, chosen);
            GiveEqToStock(receivingRoomId, numberOfTools, supplyingRoomId, SecretariesRepository._manager.RoomRepository.StockRoom,
                        chosen);
            SecretariesRepository._manager.RoomRepository.SaveData();
            SecretariesRepository.Menu();
        }

        OverviewRoom CheckIfIdInOVRoom(string supplyingRoomId)
        {
            foreach (OverviewRoom supplyingRoom in SecretariesRepository._manager.RoomRepository.OverviewRooms)
            {
                if (supplyingRoom.Id == supplyingRoomId)
                {
                    return supplyingRoom;
                }
            }
            return null;
        }
        
        OperatingRoom CheckIfIdInOPRoom(string supplyingRoomId)
        {
            foreach (OperatingRoom supplyingRoom in SecretariesRepository._manager.RoomRepository.OperatingRooms)
            {
                if (supplyingRoom.Id == supplyingRoomId)
                {
                    return supplyingRoom;
                }
            }
            return null;
        }


        public void TakeEqFromOpGiveToOv(int numberOfTools, OperatingRoom supplyingRoom1, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }


        public void TakeEqFromOvGiveToOv(int numberOfTools, OverviewRoom supplyingRoom1, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromStockGiveToOv(string supplyingRoomId, int numberOfTools, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (SecretariesRepository._manager.RoomRepository.StockRoom.Id == supplyingRoomId)
            {
                if (SecretariesRepository._manager.RoomRepository.StockRoom.DynamicEquipment[chosen] >= numberOfTools)
                {
                    SecretariesRepository._manager.RoomRepository.StockRoom.DynamicEquipment[chosen] -= numberOfTools;
                    receivingRoom.DynamicEquipment[chosen] += numberOfTools;
                }
                else
                {
                    Console.WriteLine("Not enough equipment in chosen room.");
                }
            }
        }
        

        public void GiveEqToOvRoom(string receivingRoomId, int numberOfTools, string supplyingRoomId, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                TakeEqFromStockGiveToOv(supplyingRoomId, numberOfTools, receivingRoom, chosen);
                OperatingRoom supplyingRoom = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOpGiveToOv(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }
                OverviewRoom supplyingRoom1 = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOvGiveToOv(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }


        public void TakeEqFromOvGiveToOp(int numberOfTools, OverviewRoom supplyingRoom1, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromOpGiveToOp(int numberOfTools, OperatingRoom supplyingRoom1, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }


        public void TakeEqFromStockGiveToOp(string supplyingRoomId, int numberOfTools, OperatingRoom receivingRoom, DynamicEquipment chosen )
        {
            if (SecretariesRepository._manager.RoomRepository.StockRoom.Id == supplyingRoomId)
            {
                if (SecretariesRepository._manager.RoomRepository.StockRoom.DynamicEquipment[chosen] >= numberOfTools)
                {
                    SecretariesRepository._manager.RoomRepository.StockRoom.DynamicEquipment[chosen] -= numberOfTools;
                    receivingRoom.DynamicEquipment[chosen] += numberOfTools;
                }
                else
                {
                    Console.WriteLine("Not enough equipment in chosen room.");
                    
                }
            }
        }
        
        
        public void GiveEqToOpRoom(string receivingRoomId, int numberOfTools, string supplyingRoomId, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                TakeEqFromStockGiveToOp(supplyingRoomId, numberOfTools, receivingRoom, chosen);
                OverviewRoom supplyingRoom = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOvGiveToOp(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }

                OperatingRoom supplyingRoom1 = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOpGiveToOp(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }
        
        
        public void TakeEqFromOvGiveToStock(int numberOfTools, OverviewRoom supplyingRoom1, StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromOpGiveToStock(int numberOfTools, OperatingRoom supplyingRoom1, StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
               
            }
        }

        
        public void GiveEqToStock(string receivingRoomId, int numberOfTools, string supplyingRoomId,
            StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                OverviewRoom supplyingRoom = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOvGiveToStock(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }
                OperatingRoom supplyingRoom1 = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOpGiveToStock(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }
        
    }
}
