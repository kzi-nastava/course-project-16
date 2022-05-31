using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Usi_Project.Manage
{
    public class DrugManager 
    {
        private readonly string _drugsFilename;
        private readonly string _rejectedDrugsFilename;
        private List<Drug> _drugs;
        private List<RejectedDrug> _rejectedDrugs;

        public DrugManager(string drugsFilename, string rejectedDrugsFilename)
        {
            _drugsFilename = drugsFilename;
            _rejectedDrugsFilename = rejectedDrugsFilename;
            _drugs = new List<Drug>();
            _rejectedDrugs = new List<RejectedDrug>();
        }

        public List<Drug> Drugs
        {
            get => _drugs;
            set => _drugs = value;
        }
        
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _drugs = JsonConvert.DeserializeObject<List<Drug>>(File.ReadAllText(_drugsFilename), json);

            _rejectedDrugs = JsonConvert.DeserializeObject<List<RejectedDrug>>(File.ReadAllText(_rejectedDrugsFilename), json);
        }

        public List<RejectedDrug> RejectedDrugs
        {
            get => _rejectedDrugs;
            set => _rejectedDrugs = value;
        }

        public void PrintMenu()
        {
            while (true)
            { 
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Adding new drugs to the system");
                Console.WriteLine("2) - View drugs");
                Console.WriteLine("3) - View rejected drugs");
                Console.WriteLine("x) - Back");
                Console.Write(">> ");
                string option = Console.ReadLine();
                option ??= "x";
                Console.WriteLine();
                switch (option)
                {
                    case "1":
                        AddDrug();
                        break;
                    case "2":
                        ViewDrugs();
                        break;
                    case "3":
                        ViewRejectedDrugs();
                        break;
                    case "x":
                        return;
                }
            }
        }

        private void ViewDrugs()
        {
            Drug chosen = GetDrug();
            chosen.Print();
            Console.Write("Do you want to change some ingredients ? y/n  >> ");
            string answer = Console.ReadLine();
            if (answer == "y")
                chosen.ChangeIngredients();
        }

        private void ViewRejectedDrugs()
        {
            Console.WriteLine("Rejected drugs:");
            RejectedDrug rejectedDrug = GetRejectedDrugs();
            if (rejectedDrug == null)
                return;
            
            rejectedDrug.Print();
            Console.Write("Do you want to change some ingredients ? y/n  >> ");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                rejectedDrug.ChangeIngredients();
                _drugs.Add(new Drug(rejectedDrug));
                _rejectedDrugs.Remove(rejectedDrug);
            }
        }

        private RejectedDrug GetRejectedDrugs()
        {
            if (_rejectedDrugs.Count == 0)
            {
                Console.WriteLine("\nThere is no rejected drugs.\n");
                return null;
            }
            
            Dictionary<int, RejectedDrug> drugs = new Dictionary<int, RejectedDrug>();
            int i = 1;
            foreach (var drug in _rejectedDrugs)
            {
                Console.WriteLine(i + ") " + drug.Name);
                drugs[i] = drug;
                i++;
            }
            Console.Write(">> ");
            int option = GetNumberFromCL();
            return drugs[option];
        }

        private string GetId()
        {
            while (true)
            {
                Console.Write("Input ID of new drug: >> ");
                string id = Console.ReadLine();
                if (ExistDrugId(id))
                {
                    Console.WriteLine("ID already exists, try again.");
                    continue;
                }
                return id;
            }
        }
        private void AddDrug()
        {
            string id = GetId();
            Console.Write("Input name of new drug: >> ");
            string name = Console.ReadLine();
            Console.Write("Input producer of drug: >> ");
            string producer = Console.ReadLine();
            Console.Write("Input expiration time of drug (years) >> ");
            int expirationTime = GetNumberFromCL();
            List<string> ingredients = new List<string>();
            Console.Write("How much ingredients you want to add ? >>  ");
            int numOfIngredients = GetNumberFromCL();
            for (int i = 0; i < numOfIngredients; i++)
            {
                Console.Write("Input name of new ingredient: >> ");
                string nameOfIngredient = Console.ReadLine();
                ingredients.Add(nameOfIngredient);
            }

            Drug newDrug = new Drug(id, name, producer, expirationTime, ingredients);
            _drugs.Add(newDrug);
        }

        // private void CheckVerification()
        // {
        //     foreach (var VARIABLE in _drugs.ToList())
        //     {
        //         if (VARIABLE.Verification == Verification.NOT_VERIFIED)
        //         {
        //             
        //             RejectedDrug rejectedDrug = new RejectedDrug(VARIABLE, "Ovaj lek nije dobar zbog neceg..");
        //             _rejectedDrugs.Add(rejectedDrug);
        //             _drugs.Remove(VARIABLE);
        //         }
        //     }
        // }
        

        private Drug GetDrug()
        {
            while (true)
            {
                Dictionary<int, Drug> drugs = new Dictionary<int, Drug>();
                int i = 1;
                foreach (var drug in _drugs)
                {
                    Console.WriteLine(i + ") " + drug.Name);
                    drugs[i] = drug;
                    i++;
                }
                Console.Write(">> ");
                int option = GetNumberFromCL();
                if (!drugs.ContainsKey(option))
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                return drugs[option];
            }
        }

        private int GetNumberFromCL()
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

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_drugsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _drugs);
            }
            
            using (StreamWriter file = File.CreateText(_rejectedDrugsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _rejectedDrugs);
            }
        }

        private bool ExistDrugId(string newId)
        {
            foreach (var drug in _drugs)
            {
                if (drug.Id == newId)
                    return true;
            }
            return false;
        }
    }
}