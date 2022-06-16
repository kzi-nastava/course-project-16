using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Specialized;
using Usi_Project.Users;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository
{
    public class PatientCrudService
    {
        public void CreatingPatientProfile()
        {
            Console.WriteLine("Enter the Email of the patient:");
            string patientEmail = Console.ReadLine();
            if (SecretariesRepository._manager.DirectorsRepository.CheckEmail(patientEmail))
            {
                Console.WriteLine("Entered mail already exists, try again.");
                CreatingPatientProfile();
            }
            else
            {
                if (ValidationService.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail already exists, try again.");
                    CreatingPatientProfile();
                }
                else
                {
                    if (SecretariesRepository._manager.SecretariesRepository.CheckEmail(patientEmail))
                    {
                        Console.WriteLine("Entered mail already exists, try again.");
                        CreatingPatientProfile();
                    }
                    else
                    {
                        if (SecretariesRepository._manager.PatientsRepository.CheckEmail(patientEmail))
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
                                patientAddress, patientPhone, 2, medicalRecord);
                            SecretariesRepository._manager.PatientsRepository.Patients.Add(patient);
                            using (StreamWriter file = File.CreateText(SecretariesRepository._manager.PatientsRepository.patientFileName))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Formatting = Formatting.Indented;
                                serializer.Serialize(file, SecretariesRepository._manager.PatientsRepository.Patients);
                            }

                            SecretariesRepository.Menu();
                        }
                    }
                }
            }

        }

        public void ChangingPatientProfile()
        {
            int noChanges = 0;
            int validEmail = 1;
            foreach (Patient patient in SecretariesRepository._manager.PatientsRepository.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }

            Console.WriteLine("Enter the email(x for exit) of the patient that you want to change: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                SecretariesRepository.Menu();
            }
            else if (!SecretariesRepository._manager.PatientsRepository.CheckEmail(enteredEmail))
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
                foreach (Patient patient in SecretariesRepository._manager.PatientsRepository.Patients)
                {
                    if (patient.email == enteredEmail)
                    {
                        switch (enteredOption)
                        {
                            case "1":
                                Console.WriteLine("Enter the new email of the patient: ");
                                string changedEmail = Console.ReadLine();
                                if (SecretariesRepository._manager.DirectorsRepository.CheckEmail(changedEmail))
                                {
                                    validEmail = 0;

                                }
                                else
                                {
                                    if (ValidationService.CheckEmail(changedEmail))
                                    {
                                        validEmail = 0;

                                    }
                                    else
                                    {
                                        if (SecretariesRepository._manager.SecretariesRepository.CheckEmail(changedEmail))
                                        {
                                            validEmail = 0;

                                        }
                                        else
                                        {
                                            if (SecretariesRepository._manager.PatientsRepository.CheckEmail(changedEmail))
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
                        using (StreamWriter file = File.CreateText(SecretariesRepository._manager.PatientsRepository.patientFileName))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Formatting = Formatting.Indented;
                            serializer.Serialize(file, SecretariesRepository._manager.PatientsRepository.Patients);
                        }

                        SecretariesRepository.Menu();
                    }
                }
            }
        }

        public void DeletingPatientProfile()
        {
            foreach (Patient patient in SecretariesRepository._manager.PatientsRepository.Patients)
            {
                Console.WriteLine("Email: " + patient.email);
            }

            Console.WriteLine("Enter the email(x for exit) of the patient that you want to delete: ");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == "x")
            {
                SecretariesRepository.Menu();
            }
            else if (!SecretariesRepository._manager.PatientsRepository.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                DeletingPatientProfile();
            }
            else
            {
                for (int i = 0; i < SecretariesRepository._manager.PatientsRepository.Patients.Count; i++)
                {
                    if (SecretariesRepository._manager.PatientsRepository.Patients[i].email == enteredEmail)
                    {
                        SecretariesRepository._manager.PatientsRepository.Patients.Remove(SecretariesRepository._manager.PatientsRepository.Patients[i]);
                    }
                }

                using (StreamWriter file = File.CreateText(SecretariesRepository._manager.PatientsRepository.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, SecretariesRepository._manager.PatientsRepository.Patients);
                }

                SecretariesRepository.Menu();
            }
        }
        
    }
}