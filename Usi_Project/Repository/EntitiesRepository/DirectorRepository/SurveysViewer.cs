using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.DirectorRepository
{
    public class SurveysViewer
    {
        public static void PrintResultsOfDoctorsSurvey(DoctorSurveyManager doctorSurveyManager, DoctorManager doctorManager)
        {
            foreach (var doctor in doctorManager.Doctors)
            {
                double qualityOfService = 0;
                double overallHygiene = 0;
                double areYouSatisfied = 0;
                double wouldYouRecommend = 0;
                double numOfAppointments = 0;
                var comments = "";
                foreach (var doctorSurvey in doctorSurveyManager.DocotrS)
                {
                    if (doctor.email == doctorSurvey.doctorEmail)
                    {
                        qualityOfService += doctorSurvey.qualityOfService;
                        overallHygiene += doctorSurvey.overallHygiene;
                        areYouSatisfied += doctorSurvey.areYouSatisfied;
                        wouldYouRecommend += doctorSurvey.wouldYouRecommend;
                        numOfAppointments += 1;
                        comments += doctorSurvey.comment + "\n";
                   
                    }
                }

                numOfAppointments = (numOfAppointments == 0) ? 1 : numOfAppointments; 
                Console.WriteLine("-----------------------");
                Console.WriteLine(doctor.name + " " + doctor.lastName);
                Console.WriteLine("Quality: " + qualityOfService /  numOfAppointments);
                Console.WriteLine("Overall Hygiene: " + overallHygiene / numOfAppointments);
                Console.WriteLine("Are you satisfied: " + areYouSatisfied / numOfAppointments);
                Console.WriteLine("Would you recommend: " + wouldYouRecommend / numOfAppointments);
                Console.WriteLine("Grade: " + doctor.Grade);
                Console.WriteLine("Comments:\n" + comments);
                Console.WriteLine("-----------------------");
                doctor.Grade = (overallHygiene + areYouSatisfied + wouldYouRecommend + qualityOfService) / (4 * numOfAppointments);
            }
        }

        public static void PrintResultsOfHospitalSurvey(HospitalSurveyManager hospitalSurvey)
        {
            double qualityOfService = 0;
            double overallHygiene = 0;
            double wouldYouRecommend = 0;
            double numOfGrades = 0;
            var comments = "";
            foreach (var survey in hospitalSurvey.HospitalS)
            {
                qualityOfService += survey.qualityOfService;
                overallHygiene += survey.overallHygiene;
                wouldYouRecommend += survey.wouldYouRecommend;
                comments += survey.patientEmail + ": " + survey.comment + "\n";
                numOfGrades += 1;
            }
            numOfGrades= (numOfGrades == 0) ? 1 : numOfGrades; 
            Console.WriteLine("=========================================");
            Console.WriteLine("Number of grades: " + numOfGrades);
            Console.WriteLine("Quality: " + qualityOfService /  numOfGrades);
            Console.WriteLine("Overall Hygiene: " + overallHygiene / numOfGrades);
            Console.WriteLine("Would you recommend: " + wouldYouRecommend / numOfGrades);
            Console.WriteLine("Comments:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(comments);
            Console.WriteLine("========================================");
        }

        public static void ViewThreeBestDoctors(List<Doctor> doctors)
        {

            List<Doctor> sortedDoctorsByGrade = doctors.OrderByDescending(doctor => doctor.Grade).ToList();
            PrintChosenDoctors(sortedDoctorsByGrade);

        }

        public static void ViewThreeWorseDoctors(List<Doctor> doctors)
        {
            List<Doctor> sortedDoctorsByGrade = doctors.OrderBy(doctor => doctor.Grade).ToList();
            for (int i = 0; i < sortedDoctorsByGrade.Count; i++)
            {
                if (sortedDoctorsByGrade[i].Grade == 0)
                    sortedDoctorsByGrade.Remove(sortedDoctorsByGrade[i]);
            }
            PrintChosenDoctors(sortedDoctorsByGrade);
        }

        private static void PrintChosenDoctors(List<Doctor> sortedDoctorsByGrade)
        {
            for (int i = 0; i < 3; i++)
            {
                
                Console.WriteLine("------------------------");
                Console.WriteLine(sortedDoctorsByGrade[i].name + "  " + sortedDoctorsByGrade[i].lastName);
                Console.WriteLine("Grade: " + sortedDoctorsByGrade[i].Grade);
                Console.WriteLine("------------------------");
            }
        }
    }
}