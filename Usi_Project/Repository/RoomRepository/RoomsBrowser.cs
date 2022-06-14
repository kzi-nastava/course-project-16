using System;
using System.Collections.Generic;

namespace Usi_Project.Repository
{
    public class RoomsBrowser
    {
         public static OverviewRoom FindOverviewRoom(List<OverviewRoom> _overviewRooms)
        {
            int i = 1;
            Dictionary<int, OverviewRoom> dictionary = new Dictionary<int, OverviewRoom>();
            foreach (var opp in _overviewRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see overview room: >> ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }

        public static OperatingRoom FindOperatingRoom(List<OperatingRoom> _operatingRooms)
        {
            int i = 1;
            Dictionary<int, OperatingRoom> dictionary = new Dictionary<int, OperatingRoom>();
            foreach (var opp in _operatingRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see operating room: >>  ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }

        public static RetiringRoom FindRetiringRoom(List<RetiringRoom> _retiringRooms)
        {
            int i = 1;
            Dictionary<int, RetiringRoom> dictionary = new Dictionary<int, RetiringRoom>();
            foreach (var opp in _retiringRooms)
            {
                dictionary[i] = opp;
                Console.WriteLine(i + ")  " + opp.Name);
                i++;
            }

            while (true)
            {
                Console.Write("Choose the number to see operating room: >>  ");
                int p = Convert.ToInt32(Console.ReadLine());
                if (dictionary.ContainsKey(p))
                    return dictionary[p];
                Console.WriteLine("Wrong input! Try again.");
                
            }
        }
    }
}