using System;
using Usi_Project.Manage;
using Usi_Project.Settings;
using Usi_Project.IOController;


namespace Usi_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSettings fileSettings = new FileSettings("../../../Files/doctors.json", "../../../Files/patients.json",
               "../../../Files/director.json", "../../../Files/secretaries.json",
               "../../../Files/operatingRooms.json", "../../../Files/overviewRooms.json",
               "../../../Files/retiringRooms.json", "../../../Files/appointments.json");
           Factory factory = new Factory(fileSettings);
           factory.LoadData();
           CheckInfo checkInfo = new CheckInfo(factory);
           checkInfo.PrintMenu();
           // var startTime = new DateTime(2022, 12, 12, 12, 45, 0);
           // var endTime = new DateTime(2022, 12, 12, 13, 0, 0);
           //
           // Appointment appointment = new Appointment("veljkobubnjevic@gmail.com", "bangiekg@gmail.com", startTime,
           //     endTime, "OV","op1");
           // factory.AppointmentManager.Appointment.Add(appointment);
           // factory.AppointmentManager.serialize();
           // CheckInfo checkInfo = new CheckInfo(factory);
           // checkInfo.PrintMenu();
        }
    }
}






            
