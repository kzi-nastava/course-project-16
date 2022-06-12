using System;
using System.Collections.Generic;
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
                opt = Console.ReadLine();
                if (opt == "1" || opt == "2")
                    break;
                
                Console.WriteLine("Invalid Input...");
            } 

            FillSurvey(patient, opt);
        }

        private static void FillSurvey(Patient patient, string opt)
        {
            List<DoctorSurvey> doctorSurveys = PatientManager._factory.DoctorSurveyManager.DocotrS;
            foreach (var doctorSurvey in doctorSurveys)
            {
                if (doctorSurvey.PatientEmail == patient.email)
                {
                    Console.WriteLine(doctorSurvey);
                }
            }
        }
    }
}