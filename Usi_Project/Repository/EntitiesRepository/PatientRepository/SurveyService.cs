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
            List<HospitalSurvey> hospitalSurveys = PatientManager._factory.HospitalSurveyManager.HospitalS;
            foreach (var hospitalSurvey in hospitalSurveys)
            {
                if (hospitalSurvey.PatientEmail == patient.email)
                {
                    Console.WriteLine("Usao i prosao");
                    Console.WriteLine(hospitalSurvey);
                }
            }
        }
    }
}