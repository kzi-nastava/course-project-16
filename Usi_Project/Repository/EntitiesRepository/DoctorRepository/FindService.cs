using System;
using System.Collections.Generic;
using System.Text;
using Usi_Project.Appointments;
using Usi_Project.Manage;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class FindService
    {
        public Factory _findManager;

        public FindService(Factory findManager)
        {
            _findManager = findManager;
        }

        public static string GenerateRandomString()
        {

            int length = 7;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            Console.WriteLine(str_build.ToString());
            return str_build.ToString();
        }
        public Appointment FindAppointment(DateTime time, Doctor doctor)
        {
            foreach (var appointment in _findManager.AppointmentsRepository.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime == time)
                {
                    return appointment;
                }
                
            }

            return null;
        }
        public Patient FindPatient(string patientEmail)
        {
            foreach (var patient in _findManager.PatientsRepository.Patients)
            {
                if (patient.email == patientEmail)
                {
                    return patient;
                }
                
            }

            return null;
        }
        
        public DateTime TodayAppointment()
        {
            Console.WriteLine("Enter Hour");
            int hourStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Minute");
            int minuteStart = Convert.ToInt32(Console.ReadLine());
            var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hourStart,
                minuteStart, 0);
            return time;
            
        }
        
        public void UpdateStatus(DateTime time)
        {
            List<Appointment> list = _findManager.AppointmentsRepository.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "1";
                    _findManager.Saver.SaveAppointment(list);
                    break;
                }
            }
        } 

    }
}