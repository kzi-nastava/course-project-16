using System;
using System.Collections.Generic;
using Usi_Projekat.IOController;
using Newtonsoft.Json;
using System.IO;
using Usi_Projekat.Users;

namespace Usi_Projekat.Methods
{
    class Utills
    {
        public Factory factory;
        public Utills(Factory factory)
        {
            this.factory = factory;
        }

        public Factory Factory
        {
            get => factory;
            set => factory = value;
        }

        public void readUsers(string fnPatient, string fnDoctor, string fnSecretary, string fnDirector)
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            
            
            factory.patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(fnPatient), json);
            //-----------------------------------------------------------------------------------------------------
         //   factory.doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(fnDoctor), json);
            //-----------------------------------------------------------------------------------------------------
         //   factory.secretaries= JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(fnSecretary), json);
            //-----------------------------------------------------------------------------------------------------
          //  factory.director= JsonConvert.DeserializeObject<Director>(File.ReadAllText(fnDirector), json);

        }

    }
}
