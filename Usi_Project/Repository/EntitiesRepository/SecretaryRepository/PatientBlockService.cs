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
    public class PatientBlockService
    {
        public void BlockingPatientProfile()
        {
            foreach (Patient patient in SecretaryManager._manager.PatientManager.Patients)
            {
                if (patient.Blocked == 0)
                {
                    Console.WriteLine("Email: " + patient.email);
                }
            }

            Console.WriteLine("Enter the email (x for exit) of the patient that you want to block:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                SecretaryManager.Menu();
            }
            else if (!SecretaryManager._manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                BlockingPatientProfile();
            }
            else
            {
                foreach (Patient patient in SecretaryManager._manager.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked == 0)
                    {
                        patient.Blocked = 2;

                    }
                }

                using (StreamWriter file = File.CreateText(SecretaryManager._manager.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, SecretaryManager._manager.PatientManager.Patients);
                }

                SecretaryManager.Menu();
            }
        }

        public void UnblockingPatientProfile()
        {
            foreach (Patient patient in SecretaryManager._manager.PatientManager.Patients)
            {
                if (patient.Blocked == 1)
                {
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
                SecretaryManager.Menu();
            }
            else if (!SecretaryManager._manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                UnblockingPatientProfile();
            }
            else
            {
                foreach (Patient patient in SecretaryManager._manager.PatientManager.Patients)
                {
                    if (enteredEmail == patient.email && patient.Blocked != 0)
                    {
                        patient.Blocked = 0;
                    }
                }

                using (StreamWriter file = File.CreateText(SecretaryManager._manager.PatientManager.patientFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, SecretaryManager._manager.PatientManager.Patients);
                }

                SecretaryManager.Menu();
            }
        }
    }
}