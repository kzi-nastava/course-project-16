
using System;
using System.Collections.Generic;
using Usi_Project.Appointments;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class SurveyService
    {
        public static void SurveyMenu(Patient patient)
        {
            string opt;
            while (true)
            {
                Console.WriteLine("Survey Menu:");
                Console.WriteLine("[1] Doctor Survey");
                Console.WriteLine("[2] Hospital Survey");
                Console.Write("Your Input: ");
                opt = Console.ReadLine();
                if (opt == "1" || opt == "2")
                    break;
                
                Console.WriteLine("Invalid Input...");
            } 

            FillSurvey(patient, opt);
        }

        private static void FillSurvey(Patient patient, string opt)
        {
            if (opt == "1")
            {
                DoctorSurveysDoneByPatient(patient);
                DoctorSurvey newSurvey = FillDoctorSurveyData(patient);
                List<DoctorSurvey> doctorSurveys = PatientManager._factory.DoctorSurveyManager.DocotrS;
                doctorSurveys.Add(newSurvey);
                PatientManager._factory.Saver.SaveDoctorSurvey(doctorSurveys);
            }
            else if(opt == "2")
            {
                HospitalSurveysDoneByPatient(patient);
                HospitalSurvey newSurvey = FillHospitalSurveyData(patient);
                List<HospitalSurvey> hospitalSurveys = PatientManager._factory.HospitalSurveyManager.HospitalS;
                hospitalSurveys.Add(newSurvey);
                PatientManager._factory.Saver.SaveHospitalSurvey(hospitalSurveys);
            }
        }

        private static DoctorSurvey FillDoctorSurveyData(Patient patient)
        {
            List<Appointment> forGrading = ResolveAppointmentsForGrading(patient);
            int i = 0;
            Console.WriteLine("Appointments You Can Grade: ");
            foreach (var appointment in forGrading)
            {
                Console.WriteLine("[" + i + "] " + appointment.EmailDoctor + "\tTime: " + 
                                  appointment.StartTime+" - "+appointment.EndTime);
                i++;
            }
            Console.Write("Enter Witch Appointment You Wish To Grade: ");
            int opt = Convert.ToInt32(Console.ReadLine());
            Appointment app = forGrading[opt];
            HospitalSurvey hSurvey = FillHospitalSurveyData(patient);

            return new DoctorSurvey(app.StartTime, app.EndTime, app.EmailPatient, app.EmailDoctor,
                hSurvey.qualityOfService, hSurvey.overallHygiene, hSurvey.areYouSatisfied,
                hSurvey.wouldYouRecommend, hSurvey.comment);
        }
        /*private static List<DoctorSurvey> ResolveAlreadyGradedDoctors(Patient patient)
        {
            List<DoctorSurvey> allSurveys = PatientManager._factory.DoctorSurveyManager.DocotrS;
            List<DoctorSurvey> validSurveys = new List<DoctorSurvey>();
            foreach (var survey in allSurveys)
            {
                if (patient.email == survey.patientEmail)
                {
                    validSurveys.Add(survey);
                }
            }

            return validSurveys;
        }*/
        private static List<Appointment> ResolveAppointmentsForGrading(Patient patient)
        {
            List<Appointment> allAppointments = PatientManager._factory.AppointmentManager.Appointment;
            //List<DoctorSurvey> alreadyGraded = ResolveAlreadyGradedDoctors(patient);
            List<Appointment> forGrading = new List<Appointment>();
            foreach (var appointment in allAppointments)
            {
                //foreach (var survey in alreadyGraded)
                //{
                    if (patient.email == appointment.EmailPatient && appointment.EndTime < DateTime.Now)// &&
                       // survey.doctorEmail != appointment.EmailDoctor && survey.endTime != appointment.EndTime)
                    {
                        forGrading.Add(appointment);
                    }
                //}
            }

            return forGrading;
        }
        private static HospitalSurvey FillHospitalSurveyData(Patient patient)
        {
            Console.Write("\nEnter Overall Quality Of Service: ");
            int quality = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter Overall Hygiene Level: ");
            int hygine = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nAre You Satisfied-ometer: ");
            int satisfaction = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nWould You Recommend Our Hospital: ");
            int recomendation = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nLeave a Honest Comment: ");
            string comment = Console.ReadLine();

            return new HospitalSurvey(patient.email, quality, 
                hygine, recomendation, satisfaction, comment);
        }
        private static void DoctorSurveysDoneByPatient(Patient patient)
        {
            List<DoctorSurvey> doctorSurveys = PatientManager._factory.DoctorSurveyManager.DocotrS;
            Console.WriteLine("\nYour Recent Feedback: ");
            foreach (var doctorSurvey in doctorSurveys)
            {
                if (doctorSurvey.patientEmail == patient.email)
                {
                    Console.WriteLine(doctorSurvey);
                    Console.WriteLine("\n");
                }
            }
        }
        private static void HospitalSurveysDoneByPatient(Patient patient)
        {
            List<HospitalSurvey> hospitalSurveys = PatientManager._factory.HospitalSurveyManager.HospitalS;
            Console.WriteLine("\nYour Recent Feedback: ");
            foreach (var hospitalSurvey in hospitalSurveys)
            {
                if (hospitalSurvey.patientEmail == patient.email)
                {
                    Console.WriteLine(hospitalSurvey);
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
