using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Usi_Project.DataSaver;
using Usi_Project.IOController;
using Usi_Project.Repository;
using Usi_Project.Settings;
using System.Threading;

namespace Usi_Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            
             var  fileSettings = new FileSettings(
                 "../../../Files/Roles/doctors.json", 
                 "../../../Files/Roles/patients.json",
                "../../../Files/Roles/director.json", 
                 "../../../Files/Roles/secretaries.json",
                "../../../Files/Rooms/operatingRooms.json",
                 "../../../Files/Rooms/overviewRooms.json",
                 "../../../Files/Rooms/retiringRooms.json",
                 "../../../Files/Archive/appointments.json",
                "../../../Files/Archive/anamnesa.json",
                 "../../../Files/Archive/requested.json",
                "../../../Files/Rooms/stockRoom.json", 
                 "../../../Files/Archive/timer.json",
                "../../../Files/Archive/recipes.json",
                 "../../../Files/Archive/dynamicRequests.json", 
                 "../../../Files/Drugs/drugs.json",
                 "../../../Files/Drugs/rejectedDrugs.json",
                 "../../../Files/Archive/hospitalSurvey.json",
                 "../../../Files/Archive/doctorSurvey.json",
                 "../../../Files/DayOff/DayOffRequests.json",
                 "../../../Files/Archive/notification.json");

            Saver saver = new Saver(fileSettings);
            Factory factory = new Factory(fileSettings, saver);
            factory.LoadData();
            CheckInfo checkInfo = new CheckInfo(factory);
            checkInfo.PrintMenu();
        }
    }
}