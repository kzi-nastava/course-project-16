using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Usi_Project.Users;
using System.IO;
using System.Runtime.InteropServices;


namespace Usi_Project.Repository
{
    public class AnamnesasRepository
    {
        private string _anamnesaFn;
        private List<Anamnesa> _anamnesa;
        private Factory _manager;
        
        public AnamnesasRepository()
        {
            _anamnesa = new List<Anamnesa>();
        }

        public AnamnesasRepository(string anamnesaFn, List<Anamnesa> anamnesa, Factory manager)
        {
            _anamnesaFn = anamnesaFn;
            _anamnesa = anamnesa;
            _manager = manager;
        }

        public AnamnesasRepository(string anamnesaFn, Factory manager)
        {
            _anamnesaFn = anamnesaFn;
            _anamnesa = new List<Anamnesa>();
            _manager = manager;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _anamnesa = JsonConvert.DeserializeObject<List<Anamnesa>>(File.ReadAllText(_anamnesaFn), json);

        }
        public void serialize()
        {
            using (StreamWriter file = File.CreateText(_anamnesaFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _anamnesa);
            }
        }

        public string AnamnesaFn
        {
            get => _anamnesaFn;
            set => _anamnesaFn = value;
        }

        public List<Anamnesa> Anamnesa
        {
            get => _anamnesa;
            set => _anamnesa = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }
        
        public string FormatAnamnesis(Anamnesa anamnesa)
        {
            return anamnesa.EmailDoctor + "\n" + anamnesa.EmailPatient + "\n" + anamnesa.Anamnesa1;
        }

        // Function return list<Anamnesa> for entered email of user
        // if role==0 > entered doctorEmail
        // if role==1 > entered patientEmail
        public List<Anamnesa> ResolveAnamnesisForEmail(string email, int role)
        {
            List<Anamnesa> allAnamnesis = new List<Anamnesa>();
            foreach (Anamnesa anamnesa in Anamnesa)
            {
                if (role == 0 && anamnesa.EmailDoctor == email)
                {
                    allAnamnesis.Add(anamnesa);
                }
                else if (role == 1 && anamnesa.EmailPatient == email)
                {
                    allAnamnesis.Add(anamnesa);
                }
            }
            return allAnamnesis;
        }
    }
}