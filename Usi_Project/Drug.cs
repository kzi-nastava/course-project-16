using System;
using System.Collections.Generic;

namespace Usi_Project
{

    public enum Verification
    {
        VERIFIED,
        NOT_VERIFIED
    }
    public class Drug
    {
        
        private string _id;
        private string _drugName;
        private string _producer;
        private int _expirationTime;
        private Verification _verification;
        private List<string> _ingredients;
        
        public Drug(string id, string drugName, string producer, int expirationTime, Verification verification, List<string> ingredients)
        {
            _id = id;
            _drugName = drugName;
            _producer = producer;
            _expirationTime = expirationTime;
            _verification = verification;
            _ingredients = ingredients;
        }

        public Drug(Drug drug)
        {
            _id = drug._id;
            _drugName = drug._drugName;
            _producer = drug._producer;
            _expirationTime = drug._expirationTime;
            _verification = drug._verification;
            _ingredients = drug._ingredients;
        }


        public Drug()
        {
            _id = "";
            _drugName = "";
            _producer = "";
            _verification = Verification.VERIFIED;
            _expirationTime = 0;
            _ingredients = new List<string>();
        }

        public Drug(string id, string drugName, string producer, int expirationTime, List<string> ingredients)
        {
            _id = id;
            _drugName = drugName;
            _producer = producer;
            _expirationTime = expirationTime;
            _ingredients = ingredients;
            _verification = Verification.NOT_VERIFIED;
        }
        
        public Verification Verification
        {
            get => _verification;
            set => _verification = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _drugName;
            set => _drugName = value;
        }

        public string Producer
        {
            get => _producer;
            set => _producer = value;
        }

        public int ExpirationTime
        {
            get => _expirationTime;
            set => _expirationTime = value;
        }

        public List<string> Ingredients
        {
            get => _ingredients;
            set => _ingredients = value;
        }

        private void DeleteIngredient()
        {
            _ingredients.Remove(GetIngredient());
        }

        private void AddNewIngredient()
        {
            Console.WriteLine("Input name of new ingredient: >>  ");
            string newIngredient = Console.ReadLine();
            _ingredients.Add(newIngredient);
        }

        private string GetIngredient()
        {
            Dictionary<int, string> ingredients = new Dictionary<int, string>();
            int i = 1;
            foreach (var ingredient in _ingredients)
            {
                Console.WriteLine(i + ") " + ingredient);
                ingredients[i] = ingredient;
                i++;
            }
            Console.Write(">> ");
            string option = Console.ReadLine();
            return ingredients[Int32.Parse(option)];
        }
        
        

        public void ChangeIngredients()
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Delete ingredients");
            Console.WriteLine("2) - Add new ingredients");
            string answer = Console.ReadLine();
            if (answer == "1")
                DeleteIngredient();
            else if (answer == "2")
                AddNewIngredient();
            
        }

 
        public virtual void Print()
        {
 
            Console.WriteLine("=======================");
            Console.WriteLine("ID of drug: " + Id);
            Console.WriteLine("Name: " + _drugName);
            Console.WriteLine("Producer: " + _producer);
            Console.WriteLine("Expiration time: " + _expirationTime + " years.");
            Console.WriteLine("Verification: " + _verification);
            Console.WriteLine("Ingredients:");

            foreach (var ingredient in _ingredients)
            {
                Console.WriteLine("\t" + ingredient);
            }
            Console.WriteLine("=======================");
        }
    }
    
    
}