using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Usi_Project.DataSaver;
using Usi_Project.IOController;
using Usi_Project.Repository;
using Usi_Project.Settings;
using System.Threading;

namespace Usi_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
             FileSettings  fileSettings = new FileSettings("../../../Files/doctors.json", 
                 "../../../Files/patients.json",
                "../../../Files/director.json", "../../../Files/secretaries.json",
                "../../../Files/operatingRooms.json", "../../../Files/overviewRooms.json",
                 "../../../Files/retiringRooms.json", "../../../Files/appointments.json",
                "../../../Files/anamnesa.json","../../../Files/requested.json",
                "../../../Files/stockRoom.json", "../../../Files/timer.json",
                "../../../Files/recipes.json", "../../../Files/dynamicRequests.json,", 
                 "../../../Files/drugs.json", "../../../Files/rejectedDrugs.json");

            Saver saver = new Saver(fileSettings);
            Factory factory = new Factory(fileSettings, saver);
            factory.LoadData();
            CheckInfo checkInfo = new CheckInfo(factory);
            checkInfo.PrintMenu();
        }
    }
}