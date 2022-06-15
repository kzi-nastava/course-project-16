using System;
using System.Collections.Generic;

namespace Usi_Project.Repository
{
    public class EquipmentChanger
    {
        public static void ChangeMedicalTools(RoomRepository repository, OverviewRoom overviewRoom, TimerManager timerManager)
        {
            while (true)
            {
                Console.WriteLine("Choose option or x for exit ");
                Console.WriteLine("1) Add new medical tool from stock room");
                Console.WriteLine("2) Add new medical tool from another overview room");
                Console.WriteLine("3) Remove current medical tool");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewMedicalToolFromStockRoom(repository, overviewRoom, timerManager);
                        break;
                    case "3":
                        RemoveMedicalTool(repository, overviewRoom, timerManager);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        private static void AddNewMedicalToolFromStockRoom(RoomRepository _repository, OverviewRoom overviewRoom,
            TimerManager _manager)
        {
            Dictionary<MedicalTool, int> dict = new Dictionary<MedicalTool, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 1; j < 6; j++)
            {
                string shadeName = ((MedicalTool) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());
            if (_repository.StockRoom.MedicalEquipment[(MedicalTool) choice] >= num)
            {
                var time = RoomChanger.GetTime();
                dict[(MedicalTool) choice] = num;
                Timer timer = new Timer(time, overviewRoom.Id);
                timer.MedicalDict = dict;
                _manager.Timers.Add(timer);

            }
            else
            {
                Console.WriteLine("Stock room just have " +
                                  _repository.StockRoom.MedicalEquipment[(MedicalTool) choice] + " " +
                                  ((MedicalTool) choice) + "s.");
            }
        }
        
         public static void ChangeSurgeryTools(RoomRepository repository, OperatingRoom operatingRoom, TimerManager _manager)
        {
            while (true)
            {
                Console.WriteLine("Choose option or x for exit: ");
                Console.WriteLine("1) Add new surgery tool");
                Console.WriteLine("2) Remove current surgery tool");
                Console.Write(">> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewSurgeryTool(repository, operatingRoom, _manager);
                        break;
                    case "2":
                        RemoveSurgeryTool(_manager, operatingRoom);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
        private static void AddNewSurgeryTool(RoomRepository repository, OperatingRoom operatingRoom, TimerManager _manager)
        {
            Dictionary<SurgeryTool, int> dict = new Dictionary<SurgeryTool, int>();
            Console.WriteLine("Choose what you want to add: ");
            for (int j = 0; j <7; j++)
            {
                string shadeName = ((SurgeryTool) j).ToString();
                Console.WriteLine(j + ")  " + shadeName);
            }

            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to add? >> ");
            int num = int.Parse(Console.ReadLine());

            if (repository.StockRoom.SurgeryEquipment[(SurgeryTool) choice] >= num)
            {
                var time = RoomChanger.GetTime();
                dict[(SurgeryTool) choice] = num;
                Timer timer = new Timer(time, operatingRoom.Id);
                timer.SurgeryDict = dict;
                _manager.Timers.Add(timer);

            }
            else
            {
                Console.WriteLine("Stock room just have " + 
                                  repository.StockRoom.SurgeryEquipment[(SurgeryTool) choice] + " " +
                                  ((SurgeryTool) choice) + "s.");
            }
        }
        
        private static void RemoveSurgeryTool(TimerManager _manager, OperatingRoom operatingRoom)
        {
            Dictionary<SurgeryTool, int> dict = new Dictionary<SurgeryTool, int>();
            Console.WriteLine("Choose what you want to remove: ");
            int i = 1;
            int choice;
            Dictionary<int, SurgeryTool> checkDict = new Dictionary<int, SurgeryTool>();
            while (true)
            {
                foreach (var tools in operatingRoom.SurgeryEquipments)
                {
                    checkDict[i] = tools.Key;
                    Console.WriteLine(i + ") " + tools.Key + "  Capacity: " + tools.Value);
                    i++;
                }
                Console.WriteLine(">> ");
                choice = int.Parse(Console.ReadLine());
                if (!checkDict.ContainsKey(choice))
                    Console.WriteLine("Wrong input");
                else
                    break;
            }

            Console.WriteLine("How much you want to remove? >> ");
            int num = int.Parse(Console.ReadLine());
           
            if (operatingRoom.SurgeryEquipments[checkDict[choice]] > num)
            {
                var time = RoomChanger.GetTime();
                dict[(SurgeryTool) choice] = -num;
                Timer timer = new Timer(time, operatingRoom.Id);
                timer.SurgeryDict = dict;
                _manager.Timers.Add(timer);
            }
            else
                Console.WriteLine("You don’t have that much equipment");
        }
        
        private static void RemoveMedicalTool(RoomRepository _repository, OverviewRoom overviewRoom, TimerManager _manager)
        {
            Dictionary<MedicalTool, int> dict = new Dictionary<MedicalTool, int>();
            Console.WriteLine("Choose what you want to remove: ");
            foreach (var tools in overviewRoom.Tools)
                Console.WriteLine(tools.Key + ") " + tools.Value);
            Console.WriteLine(">> ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("How much you want to remove? >> ");
            int num = int.Parse(Console.ReadLine());
            if (overviewRoom.Tools[(MedicalTool) choice] > num)
            {
                var time = RoomChanger.GetTime();
                dict[(MedicalTool) choice] = -num;
                Timer timer = new Timer(time, overviewRoom.Id);
                timer.MedicalDict = dict;
                _manager.Timers.Add(timer);
            }
            else
                Console.WriteLine("You don’t have that much equipment");
        }

        
    }
}