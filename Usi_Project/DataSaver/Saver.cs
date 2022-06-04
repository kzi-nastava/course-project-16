using System;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Appointments;
using Usi_Project.Manage;
using Usi_Project.Settings;
using Newtonsoft.Json;
using Usi_Project.Users;

namespace Usi_Project.DataSaver
{
    public class Saver
    {
        private FileSettings _fileSettings;
     

        public Saver()
        {
        }

        public Saver(FileSettings fileSettings)
        {
            _fileSettings = fileSettings;
        }

        public void SaveAppointment(List<Appointment> appointments)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.AppointmentsFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, appointments);
            }
        }
<<<<<<< Updated upstream
        public void SavePatient(List<Patient> appointments)
=======
        public void SaveRecipe(List<Recipes> recipes)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.RecipesFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, recipes);
            }
        }
        public void SavePatient(List<Patient> patients)
>>>>>>> Stashed changes
        {
            using (StreamWriter file = File.CreateText(_fileSettings.PatientFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, patients);
            }
        }
        public void SaveAnamnesa(List<Anamnesa> anamnesas)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.AnamnesaFn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, anamnesas);
            }
        }
        
        
        


    }
}