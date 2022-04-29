using Newtonsoft.Json;
using System;
using System.IO;
using Usi_Projekat.Manage;

namespace Usi_Projekat.Users
{
    public class Secretary : User
    {
        public Secretary(string email, string password, string name, string lastName, string address, string phone)
            : base(email, password, name, lastName, address, phone, Role.Secretary)
        {
        }

        public void Menu(Factory factory)
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
                    CreatingPatientProfile(factory);
                    break;
                
                case "2":
                    ChangingPatientProfile(factory);
                    break;
                
                case "3":
                    DeletingPatientProfile(factory);
                    break;
                
                case "4":
                    BlockingPatientProfile(factory);
                    break;
                
                case "5":
                    UnblockingPatientProfile(factory);
                    break;
                
                case "6":
                    //ConfirmationOfRequests();
                    break;
                
                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu(factory);
                    break;
            }
        }

        public void CreatingPatientProfile(Factory factory)
        {
            Console.WriteLine("Enter the Email of the patient:");
            string patientEmail = Console.ReadLine();
            if (factory.DirectorManager.checkEmail(patientEmail))
            {
                Console.WriteLine("Entered mail already exists, try again.");
                CreatingPatientProfile(factory);
            }
            else
            {
                if (factory.DoctorManager.checkEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail already exists, try again.");
                    CreatingPatientProfile(factory);
                }
                else
                {
                    if (factory.SecretaryManager.checkEmail(patientEmail))
                    {
                        Console.WriteLine("Entered mail already exists, try again.");
                        CreatingPatientProfile(factory);
                    }
                    else
                    {
                        if (factory.PatientManager.checkEmail(patientEmail))
                        {
                            Console.WriteLine("Entered mail already exists, try again.");
                            CreatingPatientProfile(factory);
                        }
                        else
                        {
                            Console.WriteLine("Enter the password of the patient:");
                            string patientPassword = Console.ReadLine();
                            if (factory.DirectorManager.checkPassword(patientPassword))
                            {
                                Console.WriteLine("Entered password already exists, try again.");
                                CreatingPatientProfile(factory);
                            }
                            else
                            {
                                if (factory.DoctorManager.checkPassword(patientPassword))
                                {
                                    Console.WriteLine("Entered password already exists, try again.");
                                    CreatingPatientProfile(factory);
                                }
                                else
                                {
                                    if (factory.SecretaryManager.checkPassword(patientEmail))
                                    {
                                        Console.WriteLine("Entered password already exists, try again.");
                                        CreatingPatientProfile(factory);
                                    }
                                    else
                                    {
                                        if (factory.PatientManager.checkPassword(patientEmail))
                                        {
                                            Console.WriteLine("Entered password already exists, try again.");
                                            CreatingPatientProfile(factory); 
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter the name of the patient:");
                                            string patientName = Console.ReadLine();
                                            Console.WriteLine("Enter the lastname of the patient:");
                                            string patientLastname = Console.ReadLine();
                                            Console.WriteLine("Enter the address of the patient:");
                                            string patientAddress = Console.ReadLine();
                                            Console.WriteLine("Enter the phone number of the patient:");
                                            string patientPhone = Console.ReadLine();
                                            Console.WriteLine("Enter your height:");
                                            string patientHeight = Console.ReadLine();
                                            Console.WriteLine("Enter your weight:");
                                            string patientWeight = Console.ReadLine();
                                            Console.WriteLine("Enter your previous diseases:");
                                            string patientDiseases = Console.ReadLine();
                                            Console.WriteLine("Enter your allergens:");
                                            string patientAllergens = Console.ReadLine();
                                            MedicalRecord medicalRecord = new MedicalRecord(patientHeight,
                                                patientWeight, patientDiseases, patientAllergens);
                                            Patient patient = new Patient(patientEmail, patientPassword, patientName, patientLastname,
                                                patientAddress, patientPhone, medicalRecord);
                                            factory.PatientManager.Patients.Add(patient);
                                            using (StreamWriter file = File.CreateText(factory.PatientManager.patientFileName))
                                            {
                                                JsonSerializer serializer = new JsonSerializer();
                                                serializer.Serialize(file, factory.PatientManager.Patients);
                                            }
                                            Menu(factory);
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
        }

        public void ChangingPatientProfile(Factory factory)
        {
            int validEmail = 1;
            int validPassword = 1;

            foreach (Patient patient in factory.PatientManager.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }
            Console.WriteLine("Enter the email(x for exit) of the patient that you want to change: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                Menu(factory);
            }
            else if (!factory.PatientManager.checkEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                ChangingPatientProfile(factory);
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
                foreach (Patient patient in factory.PatientManager.Patients)
                {
                    if (patient.email == enteredEmail)
                    {
                        switch (enteredOption)
                        {
                            case "1":
                                Console.WriteLine("Enter the new email of the patient: ");
                                string changedEmail = Console.ReadLine();
                                if (factory.DirectorManager.checkEmail(changedEmail))
                                {
                                    validEmail = 0;

                                }
                                else
                                {
                                    if (factory.DoctorManager.checkEmail(changedEmail))
                                    {
                                        validEmail = 0;

                                    }
                                    else
                                    {
                                        if (factory.SecretaryManager.checkEmail(changedEmail))
                                        {
                                            validEmail = 0;

                                        }
                                        else
                                        {
                                            if (factory.PatientManager.checkEmail(changedEmail))
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
                                Console.WriteLine("Enter the new password of the patient: ");
                                string changedPassword = Console.ReadLine();
                                if (factory.DirectorManager.checkPassword(changedPassword))
                                {
                                    validPassword = 0;

                                }
                                else
                                {
                                    if (factory.DoctorManager.checkPassword(changedPassword))
                                    {
                                        validPassword = 0;

                                    }
                                    else
                                    {
                                        if (factory.SecretaryManager.checkPassword(changedPassword))
                                        {
                                            validPassword = 0;

                                        }
                                        else
                                        {
                                            if (factory.PatientManager.checkPassword(changedPassword))
                                            {
                                                validPassword = 0;
                                            }
                                        }
                                    }
                                }

                                if (validPassword == 1)
                                {
                                    patient.password = changedPassword;
                                }
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
                                ChangingPatientProfile(factory);
                                break;
                        }
                    }
                }
                if (validEmail == 0 || validPassword == 0)
                {
                    Console.WriteLine("Entered email or password already exists, try again");
                    ChangingPatientProfile(factory);
                }
                else
                {
                    using (StreamWriter file = File.CreateText(factory.PatientManager.patientFileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, factory.PatientManager.Patients);
                    }
                    Menu(factory);
                }
            }
        }

        public void DeletingPatientProfile(Factory factory)
        {
            foreach (Patient patient in factory.PatientManager.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }

            Console.WriteLine("Enter the email(x for exit) of the patient that you want to delete: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                Menu(factory);
            }
            else if (!factory.PatientManager.checkEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                DeletingPatientProfile(factory);
            }
            else
            {
                for (int i = 0; i < factory.PatientManager.Patients.Count; i++)
                {
                    if (factory.PatientManager.Patients[i].email == enteredEmail)
                    {
                        factory.PatientManager.Patients.Remove(factory.PatientManager.Patients[i]);
                    }
                }

                using (StreamWriter file = File.CreateText(factory.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, factory.PatientManager.Patients);
                }

                Menu(factory);
            }
        }
        
        public void BlockingPatientProfile(Factory factory)
        {
            foreach (Patient patient in factory.PatientManager.Patients)
            {
                if (patient.Blocked == 0){
                    Console.WriteLine("Email: " + patient.email);
                }
            }

            Console.WriteLine("Enter the email (x for exit) of the patient that you want to block:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                Menu(factory);
            }
            else if (!factory.PatientManager.checkEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                BlockingPatientProfile(factory);
            }
            else{
                foreach (Patient patient in factory.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked == 0)
                    {
                        patient.Blocked = 2;

                    }
                }
                using (StreamWriter file = File.CreateText(factory.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, factory.PatientManager.Patients);
                }
                Menu(factory);
            }
        }
        
        public void UnblockingPatientProfile(Factory factory)
        {
            foreach (Patient patient in factory.PatientManager.Patients)
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
                Menu(factory);
            }
            else if (!factory.PatientManager.checkEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                UnblockingPatientProfile(factory);
            }
            else{
                foreach (Patient patient in factory.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked != 0)
                    {
                        patient.Blocked = 0;
                    }
                }
                using (StreamWriter file = File.CreateText(factory.PatientManager.patientFileName))
                {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, factory.PatientManager.Patients);
                }
                Menu(factory);
            }
        }
       /* void ConfirmationOfRequests(List<Request> requests)
        {
            foreach (Request request in requests)
            {
                Console.WriteLine(request.id);
            }
            Console.WriteLine("Unesite id zahteva koji zelite da odobrite/odbijete");
            string requestId = Console.ReadLine();
            Console.WriteLine("1) - Confirm a request");
            Console.WriteLine("2) - Deny a request");
            string requestConfirmation = Console.ReadLine();
            switch (requestConfirmation)
            {
                case "1":
                    request.confirmed = 1;
                    break;
                case "2":
                    request.confirmed = 2;
            }
        }*/
    }
}