using System;
using Usi_Projekat.IOController;
using Usi_Projekat.Manage;
using Usi_Projekat.Settings;

namespace Usi_Projekat
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSettings fileSettings = new FileSettings("doctorFilename", "patientFilename",
                "directorFilename", "secretaryFilename");
            Factory factory = new Factory(fileSettings);
            factory.loadData();
            CheckInfo checkInfo = new CheckInfo(factory);
            checkInfo.PrintMenu();
        }
    }
}






            
