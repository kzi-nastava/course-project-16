using System;
using System.Collections.Generic;
using Usi_Project.Appointments;
using Usi_Project.Manage;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class OverviewService
    {
        public Doctor doctor;
        public Factory _overviewManager;
        public ValidationService _validation;
        public FindService _finder;
        public ReferalService _referalService;
        public RecipeService _recipeService;
        
        public OverviewService(Doctor doctor, Factory overviewManager, ValidationService validation, FindService finder, ReferalService referalService, RecipeService recipeService)
        {
            this.doctor = doctor;
            _overviewManager = overviewManager;
            _validation = validation;
            _finder = finder;
            _referalService = referalService;
            _recipeService = recipeService;
        } 
        
        public void UpdateMedicalRecord(string email)
        {
            List<Patient> patients = _overviewManager.PatientManager.Patients;
            foreach (var patient in patients)
            {
                if (patient.email == email)
                {
                    Console.WriteLine("Enter what u want to change or press enter to skip");
                    Console.WriteLine("Enter height");
                    var changeHeight = Console.ReadLine();
                    if (changeHeight != "")
                    {
                        patient.MedicalRecord.height = changeHeight;
                    }
            
                    Console.WriteLine("Enter weight");
                    var changeWeight = Console.ReadLine();
                    if (changeWeight != "")
                    {
                        patient.MedicalRecord.weight = changeWeight;
                    }
                    Console.WriteLine("Enter diseases");
                    var changeDiseases = Console.ReadLine();
                    if (changeDiseases != "")
                    {
                        patient.MedicalRecord.diseases = changeWeight;
                    }
                    Console.WriteLine("Enter Alergens");
                    var changeAlergens = Console.ReadLine();
                    if (changeAlergens != "")
                    {
                        patient.MedicalRecord.allergens = changeAlergens;
                    }
                    _overviewManager.Saver.SavePatient(patients);
                    
                }
            }
        } 
        public void WriteAnamnesa(Appointment appointment)
        {
            List<Anamnesa> listAnamnesa = _overviewManager.AnamnesaManager.Anamnesa;
            Console.WriteLine("Write Anamnesa");
            string anamnesaString = Console.ReadLine();
            string id = FindService.GenerateRandomString();
            Anamnesa anamnesa = new Anamnesa(id, appointment.EmailDoctor, appointment.EmailPatient, anamnesaString);
            listAnamnesa.Add(anamnesa);
            _overviewManager.Saver.SaveAnamnesa(listAnamnesa);
        }

        public void Overview(Doctor doctor)
        {
            DateTime time = _finder.TodayAppointment();
            Appointment app = _finder.FindAppointment(time, doctor);
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Update medicalRecord.");
                Console.WriteLine("2) - Write anamnesa.");
                Console.WriteLine("3) - Referal for doctor.");
                Console.WriteLine("x) - exit.");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        UpdateMedicalRecord(app.EmailPatient);
                        break;
                    case "2":
                        WriteAnamnesa(app);
                        Console.WriteLine("Choose one of the options below: ");
                        Console.WriteLine("1) - Issuing a prescription.");
                        Console.WriteLine("2) - End overview.");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                _recipeService.CreateRecipes(app.EmailPatient);
                                _finder.UpdateStatus(app.StartTime);
                                break;
                            case "2":
                                _finder.UpdateStatus(app.StartTime);
                                break;
                        }
                        break;
                    case "3":
                        Patient patient = _finder.FindPatient(app.EmailPatient);
                        _referalService.ReferralForSpecialist(doctor, patient);
                        break;
                    case "x":
                        return;

                }
            }
        }

    }
}