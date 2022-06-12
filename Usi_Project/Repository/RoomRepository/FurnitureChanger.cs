using System;
using System.Collections.Generic;

namespace Usi_Project.Repository
{
    public class FurnitureChanger
    {
      public static void ChangeFurnitureInOpRoom(Factory factory, OperatingRoom operatingRoom)
          {
              while (true)
              {
                  Console.WriteLine("Choose option or x for exit: ");
                  Console.WriteLine("1) Add new furniture");
                  Console.WriteLine("2) Remove current furniture");
                  Console.Write(">> ");
                  switch (Console.ReadLine())
                  {
                      case "1":
                          AddNewFurniture(factory, operatingRoom);
                          break;
                      case "2":
                          RemoveCurrentFurniture(operatingRoom);
                          break;
                      case "x":
                          return;
                      default:
                          Console.WriteLine("Wrong input!");
                          break;
                  }
              }
          }
        
          public static void ChangeFurnitureInOvRoom(Factory _factory, OverviewRoom overviewRoom)
          {
              while (true)
              {
                  Console.WriteLine("Choose option or x for exit: ");
                  Console.WriteLine("1) Add new furniture");
                  Console.WriteLine("2) Remove current furniture");
                  Console.Write(">> ");
                  switch (Console.ReadLine())
                  {
                      case "1":
                          AddNewFurniture(_factory, overviewRoom);
                          break;
                      case "2":
                          RemoveCurrentFurniture(overviewRoom);
                          break;
                      case "x":
                          return;
                      default:
                          Console.WriteLine("Wrong input!");
                          break;
                  }
              }
          }
          
          private static void AddNewFurniture(Factory _manager, OperatingRoom operatingRoom)
          {
              Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
              Console.WriteLine("Choose what you want to add: ");
              for (int j = 0; j <4; j++)
              {
                  string shadeName = ((Furniture) j).ToString();
                  Console.WriteLine(j + ")  " + shadeName);
              }

              Console.WriteLine(">> ");
              int choice = int.Parse(Console.ReadLine());
              Console.WriteLine("How much you want to add? >> ");
              int num = int.Parse(Console.ReadLine());
            
              if (_manager.RoomManager.StockRoom.Furniture[(Furniture) choice] >= num)
              {
                  var time = RoomChanger.GetTime();
                  dict[(Furniture) choice] = num;
                  Timer timer = new Timer(time, operatingRoom.Id);
                  timer.FurnitureDict = dict;
                  _manager.TimerManager.Timers.Add(timer);
              }
              else
              {
                  Console.WriteLine("Stock room just have " + 
                                    _manager.RoomManager.StockRoom.Furniture[(Furniture) choice] + " " +
                                    ((Furniture) choice).ToString() + "s.");
              }
           
          }
          private static void RemoveCurrentFurniture(OverviewRoom overviewRoom)
          {
              Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
              Console.WriteLine("Choose what you want to remove: ");
              int i = 1;
              int choice;
              Dictionary<int, Furniture> checkDict = new Dictionary<int, Furniture>();
              while (true)
              {
                  foreach (var tools in overviewRoom.Furniture)
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
          }
          
          private static void RemoveCurrentFurniture(OperatingRoom operatingRoom)
          {
              Console.WriteLine("Choose what you want to remove: ");
              int i = 1;
              int choice;
              Dictionary<int, Furniture> checkDict = new Dictionary<int, Furniture>();
              while (true)
              {
                  foreach (var tools in operatingRoom.Furniture)
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
          }
          private static void AddNewFurniture(Factory _manager, OverviewRoom overviewRoom)
          {
              Dictionary<Furniture, int> dict = new Dictionary<Furniture, int>();
              Console.WriteLine("Choose what you want to add: ");
              for (int j = 0; j <4; j++)
              {
                  string shadeName = ((Furniture) j).ToString();
                  Console.WriteLine(j + ")  " + shadeName);
              }

              Console.WriteLine(">> ");
              int choice = int.Parse(Console.ReadLine());
              Console.WriteLine("How much you want to add? >> ");
              int num = int.Parse(Console.ReadLine());
            
              if (_manager.RoomManager.StockRoom.Furniture[(Furniture) choice] >= num)
              {
                  var time = RoomChanger.GetTime();
                  dict[(Furniture) choice] = num;
                  Timer timer = new Timer(time, overviewRoom.Id);
                  timer.FurnitureDict = dict;
                  _manager.TimerManager.Timers.Add(timer);
              }
              else
              {
                  Console.WriteLine("Stock room just have " + 
                                    _manager.RoomManager.StockRoom.Furniture[(Furniture) choice] + " " +
                                    ((Furniture) choice).ToString() + "s.");
              }
           
          }
          

    }
}