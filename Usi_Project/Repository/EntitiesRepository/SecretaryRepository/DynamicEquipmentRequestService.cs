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
    public class DynamicEquipmentRequestService
    {
        public void PrintAllOutOfStockDynamicTool()
        {
            Dictionary<DynamicEquipment, int> nekaList = SecretaryManager._manager.RoomManager.StockRoom.DynamicEquipment;
            foreach (OperatingRoom operatingRoom in SecretaryManager._manager.RoomManager.OperatingRooms)
            {
                nekaList = operatingRoom.PrintDynamicTools(nekaList);
            }
            foreach (OverviewRoom overviewRoom in SecretaryManager._manager.RoomManager.OverviewRooms)
            {
                nekaList = overviewRoom.PrintDynamicTools(nekaList);
            }
            
            foreach (var liste in nekaList)
            {
                if (liste.Value == 0)
                {
                    Console.WriteLine(liste.Key + " out of stock");
                }
            }
        }

        public void DynamicEquipmentRequest()
        {
            PrintAllOutOfStockDynamicTool();
            while (true)
            {
                int validEntry = 0;
                Dictionary<DynamicEquipment, int> nekaList2 = new Dictionary<DynamicEquipment, int>();
                Console.WriteLine("Enter Dynamic tool(Gauze, Buckles, Bandages, Paper, Pencils) you want to create a request for (x for exit): ");
                string toolName = Console.ReadLine().ToUpper();
                int numberOfTools; 
                switch (toolName)
                {
                    case "GAUZE":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "BUCKLES":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "BANDAGES":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "PAPER":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "PENCILS":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, numberOfTools);
                        validEntry = 1;
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("You didnt enter valid name, try again.");
                        break;
                }
                if (toolName == "X")
                {
                    break;
                }
                if (validEntry == 0)
                {
                }
                else
                {
                    DateTime currentTime = DateTime.Now;
                    DynamicRequest rikvest = new DynamicRequest(currentTime.AddHours(24), nekaList2);
                    SecretaryManager._manager.DynamicRequestManager.DynamicRequests.Add(rikvest);
                    SecretaryManager._manager.Saver.SaveDynamicRequest(SecretaryManager._manager.DynamicRequestManager.DynamicRequests);
                    break;
                }
            }
            SecretaryManager.Menu();
        }
        
    }
}