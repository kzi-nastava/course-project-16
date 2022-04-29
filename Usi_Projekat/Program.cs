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
            
            FileSettings fileSettings = new FileSettings("../../../Files/doctors.json", "../../../Files/patients.json",
                "../../../Files/director.json", "../../../Files/secretaries.json");
            Factory factory = new Factory(fileSettings);
            factory.LoadData();
            CheckInfo checkInfo = new CheckInfo(factory);
            checkInfo.PrintMenu();
        }
    }
}






            
