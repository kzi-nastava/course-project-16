using System;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Appointments;
using Usi_Project.Repository;
using Usi_Project.Settings;
using Newtonsoft.Json;
using Usi_Project.Repository.EntitiesRepository.Survey;
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
            using (StreamWriter file = File.CreateText(_fileSettings.AppointmentsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, appointments);
            }
        }
        
        public void SaveHospitalSurvey(List<HospitalSurvey> hospitalSurveys)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.HospitalSurveyFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, hospitalSurveys);
            }
        }
        public void SaveDayOff(List<DayOffRequest> dayOffRequests)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.DayOffRequestsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, dayOffRequests);
            }
        }
        
        public void SaveNotification(List<Notification> notifications)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.NotificationFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, notifications);
            }
        }

        public void SaveDoctorSurvey(List<DoctorSurvey> doctorSurveys)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.DoctorSurveyFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, doctorSurveys);
            }
        }

        public void SaveDynamicRequest(List<DynamicRequest> dynamicRequests)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.DynamicReqFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, dynamicRequests);
            }
        }

        public void SaveRecipe(List<Recipes> recipes)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.RecipesFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, recipes);
            }
        }


        public void SavePatient(List<Patient> patients)

        {
            using (StreamWriter file = File.CreateText(_fileSettings.PatientFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, patients);
            }
        }

        public  void SaveDoctors(List<Doctor> doctors)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.DoctorFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, doctors);
            }
        }

        public void SaveAnamnesa(List<Anamnesa> anamnesas)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.AnamnesaFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, anamnesas);
            }
        }

        public void SaveRequests(List<Requested> requests)
        {
            using (StreamWriter file = File.CreateText(_fileSettings.RequestedFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, requests);
            }
        }
        
    }
}