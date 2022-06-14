using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;

namespace Usi_Project.Repository

{
    public class PatientManager
    {

        private string _patientFileName;
        private List<Patient> _patients;
        public static Factory _factory;

        public PatientManager()
        {
            _patients = new List<Patient>();
        }
        public PatientManager(string patientFile, Factory manager)
        {
            _patientFileName = patientFile;
            _factory = manager;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(_patientFileName), json);
        }
        public Patient CheckPersonalInfo(string email, string password)
        {
            foreach (Patient patient in _patients)
            {
                if (email == patient.email && password == patient.password)
                {
                    return patient;
                }
               
            }
            return null;
        }

        public bool CheckEmail(string email)
        {
            foreach (Patient patient in _patients)
            {
                if (email == patient.email)
                {
                    return true;
                }
            }
            return false;
        }
        

        public List<Patient> Patients
        {
            get => _patients;
        }

        public string patientFileName
        {
            get => _patientFileName;
        }
        public void Menu(Patient patient)
        {   
            bool flagOut = AntiTrollService.AntiTroll(patient), flagInner = true;
            while (flagOut && flagInner)
            {
                // Console.Clear();
                Console.WriteLine("**********Patient Menu**********");
                Console.WriteLine("*[1] Show appointments         *");
                Console.WriteLine("*[2] Create appointment        *");
                Console.WriteLine("*[3] Update appointment        *");
                Console.WriteLine("*[4] Personal Hospital History *");
                Console.WriteLine("*[5] Search Doctors            *");
                Console.WriteLine("*[6] Update Notifications Timer*");
                Console.WriteLine("*[7] Show Notification         *");
                Console.WriteLine("*[8] Fill Survey               *");
                Console.WriteLine("*[X] Log Out                   *");
                Console.WriteLine("********************************");
                Console.Write("Your input: ");
                string inp = Console.ReadLine().ToLower();
                switch (inp)
                {
                    case "1":
                        PatientAppointmentService.ShowAppointments(patient);
                        break;
                    case "2":
                        PatientAppointmentService.CreateAppointment(patient, null);
                        break;
                    case "3":
                        ModificationRequestService.UpdateAppointment(patient);
                        break;
                    case "4":
                        MedicalRecordService.PatientHistoryMenu(patient);
                        break;
                    case "5":
                        DoctorService.SearchDoctors(patient);
                        break;
                    case "6":
                        NotificationService.UpdateNotificationsTimer(patient);
                        break;
                    case "7":
                        NotificationService.ShowRecipeNotification(patient);
                        break;
                   case "8":
                       SurveyService.SurveyMenu(patient);
                       break;
                    case "x":
                        Console.WriteLine("Loging Out...");
                        flagInner = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again.");
                        break;
                }
                if(!flagInner)
                    break;
                flagInner = AntiTrollService.AntiTroll(patient);
            }
        }
        
        public static DateTime ShowDateTimeUserInput()
        {
            DateTime dateTime;
            while (true)
            {
                Console.Write("\nEnter Year: ");
                int y = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter Month: ");
                int m = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter Day: ");
                int d = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter Hour: ");
                int h = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter Minute: ");
                int min = Convert.ToInt32(Console.ReadLine());
                if (y > 2030 || y < 2021 || m > 12 || m < 0 || d > 31 || d < 0 || h > 23 || h < 0 || min > 59 || min < 0)
                {
                    Console.WriteLine("Invalid Input, try again!");
                }
                else
                {
                    dateTime = new DateTime(y, m, d, h, min, 0);
                    break;
                }
                
            }

            return dateTime;
        }
        
    }

}