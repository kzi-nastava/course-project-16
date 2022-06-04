using System;
using System.Collections.Generic;
using Usi_Project.Manage;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class RecipeService
    {
        
        public Doctor doctor;
        public Factory _recipeManager;
        public ValidationService _validation;
        public FindService _finder;

        public RecipeService(Doctor doctor, Factory recipeManager, ValidationService validation, FindService finder)
        {
            this.doctor = doctor;
            _recipeManager = recipeManager;
            _validation = validation;
            _finder = finder;
        }
        public void CreateRecipes(string emailPatient)
        {
            List<Recipes> recipes = _recipeManager.RecipesManager.Recipes;
            Console.WriteLine("Enter cure name: ");
            string cureName = "Cure name: " + Console.ReadLine();
            Console.WriteLine("When to take medicine? ");
            string timeInstructions = "When to take medicine: " + Console.ReadLine();
            Console.WriteLine("How many times a day should he take medicine? ");
            string timesADay = "How many times a day should he take medicine: " + Console.ReadLine();
            Console.WriteLine("Choose option");
            Console.WriteLine("1) - Drink before meal.");
            Console.WriteLine("2) - Drink after meal.");
            Console.WriteLine("3) - Doesnt matter.");
            var chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    string timeRelFood = "Drink before meal.";
                    Recipes recipe = new Recipes(cureName, emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _recipeManager.Saver.SaveRecipe(recipes);
                    break;
                case "2":
                    timeRelFood = "Drink after meal.";
                    recipe = new Recipes(cureName, emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _recipeManager.Saver.SaveRecipe(recipes);
                    break;
                case "3":
                    timeRelFood = "Whenever, it is not related to food.";
                    recipe = new Recipes(cureName, emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _recipeManager.Saver.SaveRecipe(recipes);
                    break;
            }
        } 
    }
}