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
    public class ReferalConfirmationService
    {
        void ReferalOpAppointment(Doctor refferedDoctor, string patientEmail)
        {
            while (true)
            {
                DateTime startTime = ScheduleService.CreateDate();
                Console.WriteLine("Enter duration of operation in minutes");
                int duration = Convert.ToInt32(Console.ReadLine());
                DateTime endTime = startTime.AddMinutes(duration);

                if (ValidationService.CheckTime(startTime, endTime, refferedDoctor))
                {
                    var idRoom = ValidationService.GetIfFreeOverviewRoom(startTime, endTime);

                    if (idRoom != null)
                    {
                        List<Appointment> appointments = SecretariesRepository._manager.AppointmentsRepository.Appointment;
                        Appointment appointment = new Appointment(refferedDoctor.email,
                            patientEmail,
                            startTime, endTime, "OP", idRoom, "0");
                        SecretariesRepository._manager.AppointmentsRepository.Appointment.Add(appointment);

                        SecretariesRepository._manager.Saver.SaveAppointment(appointments);
                        break;
                    }
                }
                Console.WriteLine("You fell into some term ");
            }
        }
        

        void ReferalOvAppointment(Doctor refferedDoctor, string patientEmail)
        {
            while (true)
            {
                DateTime startTime = ScheduleService.CreateDate();
                DateTime endTime = startTime.AddMinutes(15);
                var idRoom = ValidationService.GetIfFreeOverviewRoom(startTime, endTime);
                if (ValidationService.CheckTime(startTime, endTime, refferedDoctor))
                {
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(refferedDoctor.email,
                            patientEmail,
                            startTime, endTime, "OV", idRoom, "0");
                        SecretariesRepository._manager.AppointmentsRepository.Appointment.Add(app);
                        SecretariesRepository._manager.Saver.SaveAppointment(SecretariesRepository._manager.AppointmentsRepository.Appointment);
                        break;
                    }

                    Console.WriteLine("All rooms are busy in this term");
                }
            }
        }

        
        public void DeleteReferal(string patientEmail)
        {
            for (int i = 0; i < SecretariesRepository._manager.PatientsRepository.Patients.Count; i++)
            {
                if (SecretariesRepository._manager.PatientsRepository.Patients[i].email == patientEmail)
                {
                    SecretariesRepository._manager.PatientsRepository.Patients[i].MedicalRecord.referral = null;
                }
            }
        }


        public string CheckEmailExist()
        {
            string patientEmail;
            while (true)
            {
                Console.WriteLine("Enter the Email of the patient:");
                patientEmail = Console.ReadLine();
                if (!SecretariesRepository._manager.PatientsRepository.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail doesn't exists, try again.");
                }
                else
                {
                    break;
                }
            }
            return patientEmail;
        }


        int PrintPatientsWithReferal()
        {
            int noReferals = 1;
            foreach (Patient patient in SecretariesRepository._manager.PatientsRepository.Patients)
            {
                if (patient.MedicalRecord.referral != null)
                {
                    noReferals = 0;
                    Console.WriteLine("Email: " + patient.email);
                }
            }

            return noReferals;
        }


        string FindDoctorForReferal(string patientEmail)
        {
            string doctorEmail = null;
            string doctorSpecialization = null;
            foreach (Patient patient in SecretariesRepository._manager.PatientsRepository.Patients)
            {
                if (patientEmail == patient.email)
                {
                    doctorEmail = patient.MedicalRecord.referral.DoctorEmail;
                    doctorSpecialization = patient.MedicalRecord.referral.DoctorSpecialisation;
                }
            }
            if (doctorEmail == null)
            {
                foreach (Doctor doctor in SecretariesRepository._manager.DoctorsRepository.Doctors)
                {
                    if (doctorSpecialization == doctor.Specialisation)
                    {
                        doctorEmail = doctor.email;
                    }
                }
            }

            return doctorEmail;
        }
        

       public void ReferalAppointment()
       {
           int noReferals = PrintPatientsWithReferal();
           if (noReferals == 1)
            {
                Console.WriteLine("No referals");
                SecretariesRepository.Menu();
            }
            else
            {
                string patientEmail = CheckEmailExist();
                string doctorEmail = FindDoctorForReferal(patientEmail);
                if (doctorEmail == null)
                {
                    Console.WriteLine("Error, try again.");
                    ReferalAppointment();
                }
                else
                {
                    Doctor refferedDoctor = null;
                    foreach (Doctor doctor in SecretariesRepository._manager.DoctorsRepository.Doctors)
                    {
                        if (doctorEmail == doctor.email)
                        {
                            refferedDoctor = doctor;
                        }
                    }
                    Console.WriteLine("Enter type ('OP' or 'OV')");
                    string type = Console.ReadLine();
                    if (type == "OV")
                    {
                        ReferalOvAppointment(refferedDoctor, patientEmail);
                    }
                    else
                    {
                        ReferalOpAppointment(refferedDoctor, patientEmail);
                    }
                    DeleteReferal(patientEmail);
                    using (StreamWriter file = File.CreateText(SecretariesRepository._manager.PatientsRepository.patientFileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(file, SecretariesRepository._manager.PatientsRepository.Patients);
                    }
                    SecretariesRepository.Menu();
                }
            }
       }
    }
}