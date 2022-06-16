using System;
using System.Collections.Generic;
using Usi_Project.Manage;

namespace Usi_Project.Repository.DrugRepository
{
    public class IOInfoDrugs
    {
        public static void AddNewDrug(DrugsRepository _drugsRepository)
        {
            string id = GetId(_drugsRepository);
            Console.Write("Input name of new drug: >> ");
            string name = Console.ReadLine();
            Console.Write("Input producer of drug: >> ");
            string producer = Console.ReadLine();
            Console.Write("Input expiration time of drug (years) >> ");
            int expirationTime = GetNumberFromCl();
            List<string> ingredients = new List<string>();
            Console.Write("How much ingredients you want to add ? >>  ");
            int numOfIngredients = GetNumberFromCl();
            for (int i = 0; i < numOfIngredients; i++)
            {
                Console.Write("Input name of new ingredient: >> ");
                string nameOfIngredient = Console.ReadLine();
                ingredients.Add(nameOfIngredient);
            }

            Drug newDrug = new Drug(id, name, producer, expirationTime, ingredients);
            _drugsRepository.AddDrug(newDrug);
            _drugsRepository.SaveData();
        }
        
        public static int GetNumberFromCl()
        {
            while(true) {
                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Must input a number!\n>> ");
                    continue;
                }
                return option;
            }
        }
        
        private static string GetId(DrugsRepository drugsRepository)
        {
            while (true)
            {
                Console.Write("Input ID of new drug: >> ");
                string id = Console.ReadLine();
                if (ExistDrugId(id, drugsRepository))
                {
                    Console.WriteLine("ID already exists, try again.");
                    continue;
                }
                return id;
            }
        }
        
        
        
        private static bool ExistDrugId(string newId, DrugsRepository drugsRepository)
        {
            foreach (var drug in drugsRepository.Drugs)
            {
                if (drug.Id == newId)
                    return true;
            }
            return false;
        }
    }
}