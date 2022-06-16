using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Repository;
using Usi_Project.Repository.DrugRepository;


namespace Usi_Project.Manage
{
    public class DrugsRepository : IDrugRepository
    {
        private readonly string _drugsFilename;
        private readonly string _rejectedDrugsFilename;
        private List<Drug> _drugs;
        private List<RejectedDrug> _rejectedDrugs;

        public DrugsRepository(string drugsFilename, string rejectedDrugsFilename)
        {
            _drugsFilename = drugsFilename;
            _rejectedDrugsFilename = rejectedDrugsFilename;
            _drugs = new List<Drug>();
            _rejectedDrugs = new List<RejectedDrug>();
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _drugs = JsonConvert.DeserializeObject<List<Drug>>(File.ReadAllText(_drugsFilename), json);

            _rejectedDrugs = JsonConvert.DeserializeObject<List<RejectedDrug>>(File.ReadAllText(_rejectedDrugsFilename), json);
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

        public List<Drug> Drugs
        {
            get => _drugs;
        }
       
        public List<RejectedDrug> RejectedDrugs
        {
            get => _rejectedDrugs;
        }
        
        public void AddDrug(Drug drug)
        {
            _drugs.Add(drug);
        }

        public void RemoveRejectedDrug(RejectedDrug drug)
        {
            _rejectedDrugs.Remove(drug);
        }
    }
}