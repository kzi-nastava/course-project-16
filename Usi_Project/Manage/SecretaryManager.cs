using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using Usi_Project.Users;

namespace Usi_Project.Manage
{
    public class SecretaryManager
    {
        private string _secretaryFilename;
        private List<Secretary> _secretaries;
        private Factory _manager;

        public SecretaryManager(string secretaryFilename, Factory factory)
        {
            _secretaryFilename = secretaryFilename;
            _manager = factory;
        }
        public void  LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(_secretaryFilename), json);

        }

        public Secretary CheckPersonalInfo(string email, string password)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email && password == secretary.password) 
                {
                    return secretary;
                }
               
            }
            return null;
        }
        
        public bool CheckEmail(string email)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void Menu()
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the patient profile.");
            Console.WriteLine("2) - Change the patient profile.");
            Console.WriteLine("3) - Delete the patient profile.");
            Console.WriteLine("4) - Block the patient profile.");
            Console.WriteLine("5) - Unblock the patient profile.");
            Console.WriteLine("6) - Confirmation of a request.");
            Console.WriteLine("x) - Exit.");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    CreatingPatientProfile();
                    break;
                
                case "2":
                    ChangingPatientProfile();
                    break;
                
                case "3":
                    DeletingPatientProfile();
                    break;
                
                case "4":
                    BlockingPatientProfile();
                    break;
                
                case "5":
                    UnblockingPatientProfile();
                    break;
                
                case "6":
                    //ConfirmationOfRequests();
                    break;
                
                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu();
                    break;
            }
        }

        public void CreatingPatientProfile()
        {
            Console.WriteLine("Enter the Email of the patient:");
            string patientEmail = Console.ReadLine();
            if (_manager.DirectorManager.CheckEmail(patientEmail))
            {
                Console.WriteLine("Entered mail already exists, try again.");
                CreatingPatientProfile();
            }
            else
            {
                if (_manager.DoctorManager.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail already exists, try again.");
                    CreatingPatientProfile();
                }
                else
                {
                    if (_manager.SecretaryManager.CheckEmail(patientEmail))
                    {
                        Console.WriteLine("Entered mail already exists, try again.");
                        CreatingPatientProfile();
                    }
                    else
                    {
                        if (_manager.PatientManager.CheckEmail(patientEmail))
                        {
                            Console.WriteLine("Entered mail already exists, try again.");
                            CreatingPatientProfile();
                        }

                        else
                        {
                            Console.WriteLine("Enter the password of a patient:");
                            string patientPassword = Console.ReadLine();
                            Console.WriteLine("Enter the name of a patient:");
                            string patientName = Console.ReadLine();
                            Console.WriteLine("Enter the lastname of a patient:");
                            string patientLastname = Console.ReadLine();
                            Console.WriteLine("Enter the address of a patient:");
                            string patientAddress = Console.ReadLine();
                            Console.WriteLine("Enter the phone number of a patient:");
                            string patientPhone = Console.ReadLine();
                            Console.WriteLine("Enter the height of a patient:");
                            string patientHeight = Console.ReadLine();
                            Console.WriteLine("Enter the weight of a patient:");
                            string patientWeight = Console.ReadLine();
                            Console.WriteLine("Enter the previous diseases of a patient:");
                            string patientDiseases = Console.ReadLine();
                            Console.WriteLine("Enter the allergens of a patient:");
                            string patientAllergens = Console.ReadLine();
                            MedicalRecord medicalRecord = new MedicalRecord(patientHeight,
                                patientWeight, patientDiseases, patientAllergens);
                            Patient patient = new Patient(patientEmail, patientPassword, patientName, patientLastname,
                                patientAddress, patientPhone, medicalRecord);
                            _manager.PatientManager.Patients.Add(patient);
                            using (StreamWriter file = File.CreateText(_manager.PatientManager.patientFileName))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Formatting = Formatting.Indented;
                                serializer.Serialize(file, _manager.PatientManager.Patients);
                            }
                            Menu();
                        }
                    }
                }
            }
                            
        }
        
        public void ChangingPatientProfile()
        {
            int noChanges = 0;
            int validEmail = 1;
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }
            Console.WriteLine("Enter the email(x for exit) of the patient that you want to change: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                ChangingPatientProfile();
            }
            else
            {
                Console.WriteLine("1) - Email");
                Console.WriteLine("2) - Password");
                Console.WriteLine("3) - Name");
                Console.WriteLine("4) - Lastname");
                Console.WriteLine("5) - Address");
                Console.WriteLine("6) - Phone");
                Console.WriteLine("x) - Exit");
                Console.WriteLine("Enter what information you want to change: ");
                string enteredOption = Console.ReadLine();
                foreach (Patient patient in _manager.PatientManager.Patients)
                {
                    if (patient.email == enteredEmail)
                    {
                        switch (enteredOption)
                        {
                            case "1":
                                Console.WriteLine("Enter the new email of the patient: ");
                                string changedEmail = Console.ReadLine();
                                if (_manager.DirectorManager.CheckEmail(changedEmail))
                                {
                                    validEmail = 0;

                                }
                                else
                                {
                                    if (_manager.DoctorManager.CheckEmail(changedEmail))
                                    {
                                        validEmail = 0;

                                    }
                                    else
                                    {
                                        if (_manager.SecretaryManager.CheckEmail(changedEmail))
                                        {
                                            validEmail = 0;

                                        }
                                        else
                                        {
                                            if (_manager.PatientManager.CheckEmail(changedEmail))
                                            {
                                                validEmail = 0;

                                            }
                                        }
                                    }
                                }
                                if (validEmail == 1)
                                {
                                    patient.email = changedEmail;
                                }
                                break;
                            
                            case "2":
                                Console.WriteLine("Enter the new password of the patient");
                                string changedPassword = Console.ReadLine();
                                patient.password = changedPassword;
                                break;


                            case "3":
                                Console.WriteLine("Enter the new name of the patient");
                                string changedName = Console.ReadLine();
                                patient.name = changedName;
                                break;


                            case "4":
                                Console.WriteLine("Enter the new lastname of the patient");
                                string changedLastname = Console.ReadLine();
                                patient.lastName = changedLastname;
                                break;


                            case "5":
                                Console.WriteLine("Enter the new address of the patient");
                                string changedAddress = Console.ReadLine();
                                patient.adress = changedAddress;
                                break;


                            case "6":
                                Console.WriteLine("Enter the new Phone of the patient");
                                string changedPhone = Console.ReadLine();
                                patient.phone = changedPhone;
                                break;

                            
                            case "x":
                                break;

                            
                            default:
                                Console.WriteLine("Invalid option entered, try again");
                                noChanges = 1;
                                ChangingPatientProfile();
                                break;
                        }
                    }
                }
                if (validEmail == 0)
                {
                    Console.WriteLine("Entered email already exists, try again");
                    ChangingPatientProfile();
                }
                else
                {
                    if (noChanges == 0)
                    {
                        using (StreamWriter file = File.CreateText(_manager.PatientManager.patientFileName))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Formatting = Formatting.Indented;
                            serializer.Serialize(file, _manager.PatientManager.Patients);
                        }

                        Menu();
                    }
                }
            }
        }

        public void DeletingPatientProfile()
        {
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }

            Console.WriteLine("Enter the email(x for exit) of the patient that you want to delete: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                DeletingPatientProfile();
            }
            else
            {
                for (int i = 0; i < _manager.PatientManager.Patients.Count; i++)
                {
                    if (_manager.PatientManager.Patients[i].email == enteredEmail)
                    {
                        _manager.PatientManager.Patients.Remove(_manager.PatientManager.Patients[i]);
                    }
                }

                using (StreamWriter file = File.CreateText(_manager.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, _manager.PatientManager.Patients);
                }

                Menu();
            }
        }
        
        public void BlockingPatientProfile()
        {
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                if (patient.Blocked == 0){
                    Console.WriteLine("Email: " + patient.email);
                }
            }

            Console.WriteLine("Enter the email (x for exit) of the patient that you want to block:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                BlockingPatientProfile();
            }
            else{
                foreach (Patient patient in _manager.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked == 0)
                    {
                        patient.Blocked = 2;

                    }
                }
                using (StreamWriter file = File.CreateText(_manager.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, _manager.PatientManager.Patients);
                }
                Menu();
            }
        }
        
        public void UnblockingPatientProfile()
        {
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                if (patient.Blocked == 1){
                    Console.WriteLine("Email: " + patient.email + " Blocked by System");
                }
                else if (patient.Blocked == 2)
                {
                    Console.WriteLine("Email: " + patient.email + " Blocked by Secretary");
                }
            }
            Console.WriteLine("Enter the email (x for exit) of the patient that you want to unblock:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                UnblockingPatientProfile();
            }
            else{
                foreach (Patient patient in _manager.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked != 0)
                    {
                        patient.Blocked = 0;
                    }
                }
                using (StreamWriter file = File.CreateText(_manager.PatientManager.patientFileName))
                {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(file, _manager.PatientManager.Patients);
                }
                Menu();
            }
        }
        
      /*   void ConfirmationOfRequests()
        {
            int noChanges = 0;
            foreach (Request request in _manager.RequestManager.Requests)
            {
            (if request.confirmed == 0)
            {
                Console.WriteLine("request id: " + request.id);
            }
            }
            Console.WriteLine("Enter id (x for exit) of the request you want to confirm/deny:");
            string requestId = Console.ReadLine();
            if (requestId == ("x"))
            {
                Menu();
            }
            else if (!_manager.RequestManager.CheckId(requestId))
                //checkId(requestId) should check if request.confirmed is 0 too
            {
                Console.WriteLine("Invalid id entered, try again");
                ConfirmationOfRequests();
            }
            else
            {
                Console.WriteLine("1) - Confirm a request");
                Console.WriteLine("2) - Deny a request");
                Console.WriteLine("x) - Exit");
                string requestConfirmation = Console.ReadLine();
                switch (requestConfirmation)
                {
                    case "1":
                        foreach (Request request in _manager.RequestManager.Requests)
                        {
                            if (requestId == request.id)
                            {
                                request.confirmed = 1;
                                //status 1 - changing the appointment
                                if (request.status == 1)
                                {
                                    foreach (Appointment appointment in _manager.AppointmentManager.Appointments)
                                    {
                                        if (appointment.startTime == request.startTime &&
                                            appointment.EndTime == request.endTime)
                                        {
                                            appointment.StartTime = request.changedStartTime;
                                            appointment.EndTime = request.changedEndTime;
                                        }
                                    }
                                }
                                else
                                //status 2 - deleteing
                                {
                                    for (int i = 0; i < _manager.AppointmentManager.Appointments.Count; i++)
                                    {
                                        if (_manager.AppointmentManager.Appointments[i].startTime == request.startTime &&
                                            _manager.AppointmentManager.Appointments[i].EndTime == request.endTime)
                                        {
                                            _manager.AppointmentManager.Appointments.Remove(_manager.AppointmentManager.Appointments[i]);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case "2":
                        foreach (Request request in _manager.RequestManager.Requests)
                        {
                            if (requestId == request.id)
                            {
                                request.confirmed = 2;
                            }
                        }
                        break;

                    case "x":
                        break;

                    default:
                        Console.WriteLine("Invalid option entered, try again");
                        noChanges = 1;
                        ConfirmationOfRequests();
                        break;
                }
                if (noChanges == 0)
                {
                    using (StreamWriter file = File.CreateText(_manager.AppointmentManager.requestFileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(file, _manager.AppointmentManager.Appointments);
                    }
                    using (StreamWriter file = File.CreateText(_manager.RequestManager.requestFileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(file, _manager.RequestManager.Requests);
                    }
                    Menu();
                }
            }
        } */
       
        
        
    }
}