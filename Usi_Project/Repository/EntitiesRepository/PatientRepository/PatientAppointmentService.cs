
using System;
using System.Collections.Generic;
using Usi_Project.Appointments;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class PatientAppointmentService
    {
               
        public static void ShowAppointments(Patient patient)
        {
            int i = 1;
            Console.WriteLine("---------------------");
            Console.WriteLine("Current Appointments:");
            Console.WriteLine("---------------------");
            foreach (Appointment appointment in PatientsRepository._factory.AppointmentsRepository.Appointment)
            {
                if (appointment.EmailPatient == patient.email)
                {
                    Console.Write("[" + i + "]");
                    appointment.PrintAppointment();
                    Console.WriteLine("---------------------");
                    i =+ 1;
                }
            }
        }
        public static void CreateAppointment(Patient patient, Doctor doctorForAppoint)
        {
            string appType;
            Console.Write("Do You Wish To Manually Make Appointment?[Y/N]: ");
            appType = Console.ReadLine();
            if (appType == "N" || appType == "n")
            {
                AutoAppointment(patient, doctorForAppoint);
            }
            else
            {
                if (doctorForAppoint is null)
                    doctorForAppoint = ResolveAppointmentDataService.ResolveDoctorForAppointment();
                

                DateTime appointTime;
                appointTime = ResolveAppointmentDataService.ResolveTimeForAppointment(doctorForAppoint);

                string roomId;
                roomId = ResolveAppointmentDataService.ResolveRoomForAppointment(appointTime);
                
                List<Appointment> appointments = PatientsRepository._factory.AppointmentsRepository.Appointment;
                Appointment app = new Appointment(doctorForAppoint.email,
                    patient.email,appointTime, appointTime.AddMinutes(15), "OV", roomId,"0");
                appointments.Add(app);
                PatientsRepository._factory.Saver.SaveAppointment(appointments);

                Requested requested = new Requested(patient.email, appointTime, appointTime.AddMinutes(15), "1");
                List<Requested> reqList = PatientsRepository._factory.RequestManager.Requested;
                reqList.Add(requested);
                PatientsRepository._factory.Saver.SaveRequests(reqList);
            }
        }
        public static void AutoAppointment(Patient patient, Doctor docForAppoint)
        {
            if (docForAppoint is null)
            {
                docForAppoint = ResolveAppointmentDataService.ResolveDoctorForAppointment(2);
            }
            Console.WriteLine("Doctor: " + docForAppoint);
            Console.WriteLine("Enter Date & Time, Time Delta Of 8 Hours Will Be Taken Into Consideration");
            DateTime appointTime = PatientsRepository.ShowDateTimeUserInput();
            //DateTime timeDelta = PatientsRepository.ShowDateTimeUserInput();
           // DateTime appointTime = ResolveAppointmentDataService.AutoTimeForAppointment(docForAppoint, timeDelta);
            
            string roomId = ResolveAppointmentDataService.ResolveRoomForAppointment(appointTime);
            

             List<Appointment> appointments = PatientsRepository._factory.AppointmentsRepository.Appointment;
             Appointment appointment = new Appointment(docForAppoint.email,
                 patient.email,
                 appointTime, appointTime.AddMinutes(15), "OV", roomId, "0");
             appointments.Add(appointment);
             PatientsRepository._factory.Saver.SaveAppointment(appointments);

             List<Requested> reqList = PatientsRepository._factory.RequestManager.Requested;
             Requested requested = new Requested(patient.email, appointTime, 
                 appointTime.AddMinutes(15), "1");
             reqList.Add(requested);
             PatientsRepository._factory.Saver.SaveRequests(reqList);
        }
        
        

    }
}
