using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Project.Users;
using System.IO;

namespace Usi_Project.Manage
{
    public class RecipesManager
    {
        private string _recipesFilename;
        private List<Recipes> _recipes;
        private Factory _manager;
        
        
        public RecipesManager()
        {
            _recipes = new List<Recipes>();
        }

        public RecipesManager(string appointmentFilename, List<Recipes> appointment, Factory manager)
        {
            _recipesFilename = appointmentFilename;
            _recipes = appointment;
            _manager = manager;
        }

        public RecipesManager(string appointmentFilename, Factory manager)
        {
            _recipesFilename = appointmentFilename;
            _recipes = new List<Recipes>();
            _manager = manager;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _recipes = JsonConvert.DeserializeObject<List<Recipes>>(File.ReadAllText(_recipesFilename), json);

        }

        public void serialize()
        {
            using (StreamWriter file = File.CreateText(_recipesFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _recipes);
            }
        }

        public string RecipesFilename
        {
            get => _recipesFilename;
            set => _recipesFilename = value;
        }

        public List<Recipes> Recipes
        {
            get => _recipes;
            set => _recipes = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
    }
}