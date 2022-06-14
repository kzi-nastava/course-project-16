
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Specialized;
using Usi_Project.Users;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository
{
    public class EmergencyAppointmentService
    {
        public void PrintPatientsEmail()
       {
           foreach (Patient patient in SecretaryManager._manager.PatientManager.Patients)
           {
               Console.WriteLine("Email: " + patient.email);
           }
       }

        
        public void EmergencyAppointment()
        {
            PrintPatientsEmail();
            Console.WriteLine("Enter the email (x for exit) of a patient that  you want to schedule appointment for:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                SecretaryManager.Menu();
            }
            else if (!SecretaryManager._manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                EmergencyAppointment();
            }
            else
            ChooseSpecialization(enteredEmail);
        }
        
        
        public void ChooseSpecialization(string enteredEmail)
        {
            Console.WriteLine("Chose the Doctor specialization");
            Console.WriteLine("1) - Cardiology");
            Console.WriteLine("x) - exit");
            string chosenSpecialization = Console.ReadLine();
            switch (chosenSpecialization)
            {
                case "1":
                    EmergencyAppointmentSpecializationBased("Cardiology", enteredEmail);
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    EmergencyAppointment();
                    break;
            }
            SecretaryManager.Menu();
        }
        
        
        void EmergencyAppointmentSpecializationBased(string specialization, string enteredEmail)
        {
            Console.WriteLine("Enter the duration of a Appointment: ");
            int appointmentDuration = Convert.ToInt32(Console.ReadLine());
            foreach (Doctor doctor in SecretaryManager._manager.DoctorManager.Doctors)
            {
                if (specialization == doctor.Specialisation)
                {
                    DateTime appointTime = DateTime.Now;
                    int appointmentFound = FreeTermFound(doctor, appointTime, appointmentDuration, enteredEmail);
                    if (appointmentFound == 0)
                    {
                        RescheduleAppointment(doctor, appointTime, enteredEmail);
                    }
                    break;
                }
            }
        }
        
        
        

        
        int FreeTermFound(Doctor doctor, DateTime appointTime, int appointmentDuration, string enteredEmail)
        {
           int appointmentFound = 0;
            for (int i = 0; i < 120; i += 10)
            {
                if (ValidationService.CheckTime(appointTime.AddMinutes(i),
                        appointTime.AddMinutes(i + appointmentDuration), doctor))
                {
                    var idRoom = ValidationService.GetIfFreeOverviewRoom(appointTime.AddMinutes(i),
                        appointTime.AddMinutes(i + appointmentDuration));
                    if (idRoom != null)
                    {
                        Appointment appointment = new Appointment(doctor.email,
                            enteredEmail,
                            appointTime.AddMinutes(i), appointTime.AddMinutes(i + appointmentDuration),
                            "OP", idRoom, "0");
                        SecretaryManager._manager.AppointmentManager.Appointment.Add(appointment);
                        appointmentFound = 1;
                        SecretaryManager._manager.Saver.SaveAppointment(SecretaryManager._manager.AppointmentManager.Appointment);
                        break;
                    }
                }
            }

            return appointmentFound;
        }
        
        
        public void RescheduleAppointment(Doctor doctor, DateTime appointTime, string enteredEmail)
        {
            List<Appointment> scheduledAppointments = FindScheduledAppointments(doctor, appointTime);
            int indexOfAppointment = ChooseTermToReschedule(scheduledAppointments);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (i == indexOfAppointment)
                {
                    if (scheduledAppointments[i].Type == "OV")
                    {
                        ScheduleTypeOvAppointment(doctor, scheduledAppointments, i);
                    }
                    else
                    {
                        ScheduleTypeOpAppointment(doctor, scheduledAppointments, i);
                    }
                    ScheduleEmergencyAppointment(doctor, enteredEmail, scheduledAppointments, i);
                }
            }
            
        }

        
        List<Appointment> FindScheduledAppointments(Doctor doctor, DateTime appointTime)
        {
            List<Appointment> scheduledAppointments = new List<Appointment>();
            foreach (Appointment appointment in SecretaryManager._manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email &&
                    appointment.StartTime > appointTime &&
                    appointment.StartTime < appointTime.AddHours(2))
                {
                    scheduledAppointments.Add(appointment);
                }

            }
            return scheduledAppointments;
        }
        
        
        int ChooseTermToReschedule(List<Appointment>scheduledAppointments)
        {
            int indexOfAppointment;
            while (true)
            {
                Console.WriteLine("Enter the appointment you want to reschedule");
                for (int i = 0; i < scheduledAppointments.Count; i++)
                {
                    Console.WriteLine(i + ")" + "Start time: " + scheduledAppointments[i].StartTime
                                      + " end time: " + scheduledAppointments[i].EndTime);
                }

                 indexOfAppointment = Convert.ToInt32(Console.ReadLine());
                if (indexOfAppointment < 0 || indexOfAppointment > scheduledAppointments.Count)
                {
                    Console.WriteLine("Invalid option, try again.");
                }
                else
                {
                    break;
                }
            }
            return indexOfAppointment;
        }

        void ScheduleTypeOvAppointment(Doctor doctor, List<Appointment> scheduledAppointments, int i)
        {
            while (true)
            {
                DateTime startTime = ScheduleService.CreateDate();
                DateTime endTime = startTime.AddMinutes(15);
                var idRoom =
                    ValidationService.GetIfFreeOverviewRoom(startTime, endTime);
                if (ValidationService.CheckTime(startTime, endTime, doctor))
                {
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(doctor.email,
                            scheduledAppointments[i].EmailPatient,
                            startTime, endTime, "OV", idRoom, "0");
                        SecretaryManager._manager.AppointmentManager.Appointment.Add(app);
                        SecretaryManager._manager.Saver.SaveAppointment(SecretaryManager._manager.AppointmentManager.Appointment);
                        break;
                    }

                    Console.WriteLine("All rooms are busy in this term");
                }
            }
        }

        void ScheduleTypeOpAppointment(Doctor doctor, List<Appointment> scheduledAppointments, int i)
        {
            while (true)
            {
                DateTime startTime = ScheduleService.CreateDate();
                Console.WriteLine("Enter duration of operation in minutes");
                int duration = Convert.ToInt32(Console.ReadLine());
                DateTime endTime = startTime.AddMinutes(duration);

                if (ValidationService.CheckTime(startTime, endTime, doctor))
                {
                    var idRoom = ValidationService.GetIfFreeOverviewRoom(startTime, endTime);
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(doctor.email,
                            scheduledAppointments[i].EmailPatient,
                            startTime, endTime, "OP", idRoom, "0");
                        SecretaryManager._manager.AppointmentManager.Appointment.Add(app);
                        SecretaryManager._manager.Saver.SaveAppointment(SecretaryManager._manager.AppointmentManager.Appointment);
                        break;
                    }
                }

                Console.WriteLine("You fell into some term ");
            }
        }

        void ScheduleEmergencyAppointment(Doctor doctor, string enteredEmail, List<Appointment> scheduledAppointments,
            int i)
        {
            Appointment appointment = new Appointment(doctor.email,
                enteredEmail,
                scheduledAppointments[i].StartTime,
                scheduledAppointments[i].EndTime, scheduledAppointments[i].Type,
                scheduledAppointments[i].IdRoom, "0");
            SecretaryManager._manager.AppointmentManager.Appointment.Add(appointment);
            SecretaryManager._manager.Saver.SaveAppointment(SecretaryManager._manager.AppointmentManager.Appointment);
        }
        
    }
}
