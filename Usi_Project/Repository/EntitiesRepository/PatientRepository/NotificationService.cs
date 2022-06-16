using System;
using System.Collections.Generic;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class NotificationService
    {
        public static void UpdateNotificationsTimer(Patient patient)
        {
            List<Patient> patientsList = PatientsRepository._factory.PatientsRepository.Patients;
            Console.Write("Enter New Notification Timer: ");
            int newNotificationTimer = Convert.ToInt32(Console.ReadLine());
            foreach (var pat in patientsList)
            {
                if (pat.email == patient.email)
                {
                    pat.NotificationTimer = newNotificationTimer;
                }
            }
            PatientsRepository._factory.Saver.SavePatient(patientsList);
        }

        private static List<Recipes> RecipesForPatient(Patient patient)
        {
            List<Recipes> recipesList = PatientsRepository._factory.RecipesManager.Recipes;
            List<Recipes> recipesForCurrentPatient = new List<Recipes>();
            foreach (var recipe in recipesList)
            {
                if (recipe.emailPatient == patient.email)
                {
                    recipesForCurrentPatient.Add(recipe);
                }
            }

            return recipesForCurrentPatient;
        }

        public static void ShowRecipeNotification(Patient patient)
        {
            List <Recipes> recipeList= RecipesForPatient(patient);
            Console.WriteLine("broj "+recipeList.Count);
            foreach (var recipe in recipeList)
            {
                int numPerDay = Convert.ToInt32(recipe.timesADay.Split(":")[1]);    //2x     12h
                int takeEveryNotification = 24 / numPerDay - patient.NotificationTimer;     //10h
                DateTime currentDate = DateTime.Now;    //11
                Console.WriteLine("Notification Timer: " + patient.NotificationTimer);
                if (currentDate.Hour%12>takeEveryNotification && 
                    currentDate.Hour%12<takeEveryNotification+patient.NotificationTimer)
                {
                    Console.WriteLine(recipe.cureName);
                    Console.WriteLine(recipe.timeInstructions + "\n" + recipe.timeRelFood);
                }
            }
        }

    }
}
