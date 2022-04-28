using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using Usi_Projekat.Manage;

namespace Usi_Projekat.Users
{
    public class Secretary : User
    {

        public Secretary(string email, string password, string name, string lastName, string adress, string phone,
            string id)
            : base(email, password, name, lastName, adress, phone, id, Role.Secretary)
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
                    ConfirmationOfRequests();
                    break;
                
                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Meni(patients);
                    break;
            }
        }

        public void CreatingPatientProfile(List<Patient> patients)
        {
            int validMail = 1;
            int validPassword = 1;
            Console.WriteLine("Enter the Email of the patient:");
            string patientEmail = Console.ReadLine();
            foreach (Patient patient in patients)
            {
                if (patient.email == patientEmail)
                {
                    validMail = 0;
                    break;
                }
            }

            if (validMail == 0)
            {
                Console.WriteLine("Entered mail already exists, try again.");
                CreatingPatientProfile(patients);

            }
            else
            {
                Console.WriteLine("Enter the password of the patient:");
                string patientPassword = Console.ReadLine();
                foreach (Patient patient in patients)
                {
                    if (patient.password == patientPassword)
                    {
                        validPassword = 0;
                        break;
                    }
                }
                if (validPassword == 0)
                {
                    Console.WriteLine("Entered password already exists, try again");
                    CreatingPatientProfile(patients);
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
                    Console.WriteLine("Enter the Id of the patient:");
                    string patientId = Console.ReadLine();
                    Patient patient = new Patient(patientEmail, patientPassword, patientName, patientLastname,
                        patientAddress, patientPhone, patientId);
                    // string json = JsonConvert.SerializeObject(patient, Formatting.Indented);
                    patients.Add(patient);
                    using (StreamWriter file = File.CreateText("../patients.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, patients);
                    }

                    Meni(patients);
                }
            }
        }

        public void ChangingPatientProfile(List<Patient> patients)
        {
            
                foreach (Patient patient in patients)
                {
                    Console.WriteLine(patient.email);
                }
                int validPassword = 1;
                int validEmail = 1;

                Console.WriteLine("Enter the email of the patient that you want to change: ");
                string enteredEmail = Console.ReadLine();
                Console.WriteLine("1) - Email");
                Console.WriteLine("2) - Password");
                Console.WriteLine("3) - Name");
                Console.WriteLine("4) - Lastname");
                Console.WriteLine("5) - Address");
                Console.WriteLine("6) - Phone");
                Console.WriteLine("7) - Id");
                Console.WriteLine("x) - Exit");
                Console.WriteLine("Enter what information you want to change: ");
                string enteredOption = Console.ReadLine();
                foreach (Patient patient in patients)
                {
                    if (patient.email == enteredEmail)
                    {
                        switch (enteredOption)
                        {
                            case "1":
                                
                                Console.WriteLine("Enter the new email of the patient: ");
                                string changedEmail = Console.ReadLine();
                                foreach (Patient patientCheck in patients)
                                {
                                    if (patientCheck.email == changedEmail)
                                    {
                                        validEmail = 0;
                                        break;
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
                                foreach (Patient patientCheck in patients)
                                {
                                    if (patientCheck.password == changedPassword)
                                    {
                                        validPassword = 0;
                                        break;
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
                                ChangingPatientProfile(patients);
                                break;
                                
                        }
                    }
                }

                if (validEmail == 0 || validPassword == 0)
                {
                    Console.WriteLine(" Enetered email or password already exists, try again");
                    ChangingPatientProfile(patients);
                }
                else
                {
                    using (StreamWriter file = File.CreateText("../patients.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, patients);
                        Meni(patients);
                    }
                }
        }


        public void DeletingPatientProfile(List<Patient> patients)
        {
            foreach (Patient patient in patients)
                {
                    Console.WriteLine(patient.email);
                }
                Console.WriteLine("Enter the email of the patient that you want to delete: ");
                string enteredEmail = Console.ReadLine();
                for (int i = 0; i < patients.Count; i++)
                {
                    if (patients[i].email == enteredEmail)
                    {
                        patients.Remove(patients[i]);
                    }
                }
                
                using (StreamWriter file = File.CreateText("../patients.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, patients);
                }
                Meni(patients);
        }
        
        public void BlockingPatientProfile(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                if (patient.Blocked == 0){
                    Console.WriteLine(patient.email);
                }
            }

            Console.WriteLine("Enter the email (x for exit) of the patient that you want to block:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                
            }
            else{
                int found = 0;
                foreach (Patient patient in patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked == 0)
                    {
                        patient.Blocked = 2;
                        found = 1;

                    }
                }

                if (found == 0)
                {
                    Console.WriteLine("You did not enter a existing email.");
                }
                else
                {
                    using (StreamWriter file = File.CreateText("../patients.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, patients);
                    }
                }
            }
            Meni(patients);
        }
        
        public void UnblockingPatientProfile(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                if (patient.Blocked == 1){
                    Console.WriteLine(patient.email + " Blocked by System");
                    
                }
                else if (patient.Blocked == 2)
                {
                    Console.WriteLine(patient.email + " Blocked by Secretary");
                }
            }
            Console.WriteLine("Enter the email (x for exit) of the patient that you want to unblock:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
            }
            else{
                int found = 0;
                foreach (Patient patient in patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked != 0)
                    {
                        patient.Blocked = 0;
                        found = 1;

                    }
                }

                if (found == 0)
                {
                    Console.WriteLine("You did not enter a existing email.");
                }
                else
                {
                    using (StreamWriter file = File.CreateText("../patients.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, patients);
                    }
                }
            }
            Meni(patients);
        }
        void ConfirmationOfRequests()
        {
            
        }
    }
}