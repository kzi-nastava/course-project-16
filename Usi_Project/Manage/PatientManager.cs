using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;
using Usi_Project.Appointments;

namespace Usi_Project.Manage

{
    public class PatientManager
    {
        private string _patientFileName;
        private List<Patient> _patients;
        private Factory _factory;

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
            bool flag, flagIner = true;
            flag = AntiTroll(patient);
            while (flag && flagIner)
            {
                string inp;
                Console.Clear();
                Console.WriteLine("**********Patient Menu**********");
                Console.WriteLine("*[1] Show appointments         *");
                Console.WriteLine("*[2] Create appointment        *");
                Console.WriteLine("*[3] Update appointment        *");
                Console.WriteLine("*[4] Personal Hospital History *");
                Console.WriteLine("*[5] Search Doctors            *");
                Console.WriteLine("*[6] Update Notifications      *");
                Console.WriteLine("*[7] Fill Survey               *");
                Console.WriteLine("*[X] Log Out                   *");
                Console.WriteLine("********************************");
                Console.Write("Your input: ");
                inp = Console.ReadLine().ToLower();
                switch (inp)
                {
                    // CRUD, Anti-Trol, izmena/brisanje terminna(1, 2, 3 + at)
                    case "1":
                        ShowAppointments(patient);
                        break;
                    case "2":
                        CreateAppointment(patient);
                        break;
                    case "3":
                        UpdateAppointment(patient);
                        break;
                    case "4":
                        PatientHistory(patient);
                        break;
//                    case "5":
//                        SearchDoctors();
//                        break;
//                    case "6":
//                        ChangeNotifications();
//                        break;
//                   case "7":
//                        FillSurvey();
//                        break;
                    case "x":
                        Console.WriteLine("Loging Out...");
                        flagIner = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again.");
                        break;
                }

                flag = AntiTroll(patient);
            }
        }

        public void PatientHistoryMenu(Patient patient)
        {
            bool flag = true;
            while (flag)
            {
                string inp;
                Console.WriteLine("************************");
                Console.WriteLine("*     History Menu     *");
                Console.WriteLine("* [1] Show All History *");
                Console.WriteLine("* [2] Search History   *");
                Console.WriteLine("* [X] For aborting     *");
                Console.WriteLine("************************");
                Console.Write("Your input: ");
                inp = Console.ReadLine();
                if (inp == "X")
                    inp = inp.ToLower();
                switch (inp)
                {
                    case "1":
                        PatientHistory(patient);
                        break;
                    case "2":
                        SearchPatientHistory(patient);
                        break;
                    case "x":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again...");
                        break;
                }

            }
        }

        public void PatientHistory(Patient patient)
        {
            List<Anamnesa> li = _factory.AnamnesaManager.ResolveAnamnesisForEmail(patient.email, 1);
            Console.WriteLine("**************************");
            Console.WriteLine("List of Anamnesis:");
            foreach (Anamnesa anamnesa in li)
            {
                Console.WriteLine(anamnesa.ToString());
            }

            string inp;
            Console.Write("Do You Wish To Sort Anamnese[Y/N]: ");
            inp = Console.ReadLine();
            if (inp.ToLower() == "y")
            {
                li.Sort(delegate(Anamnesa x, Anamnesa y) { return x.EmailDoctor.CompareTo(y.EmailDoctor);});
                foreach (Anamnesa anamnesa in li)
                {
                    Console.WriteLine(anamnesa.ToString());
                }
            }
        }

        public void SearchPatientHistory(Patient patient)
        {
            List<Anamnesa> li = _factory.AnamnesaManager.ResolveAnamnesisForEmail(patient.email, 1);
            while (true)
            {
                string inp;
                Console.Write("Enter Keyword: ");
                inp = Console.ReadLine();
                foreach (Anamnesa anamnesa in li)
                {
                    if (anamnesa.Anamnesa1.Contains(inp))
                    {
                        Console.WriteLine(anamnesa.ToString());
                    }
                }
                Console.Write("Do You Wish To Continue Search[Y/N]: ");
                inp = Console.ReadLine();
                if (inp.ToLower() != "y")
                {
                    break;
                }

            }
        }
        public bool CheckBlockedStatus(Patient patient)
        {
            if (patient.Blocked == 2)
            {
                Console.WriteLine("Sorry, Your Account Has Been Blocked" +
                                  "Contact Support, Much Luck!");
                return false;
            }

            return true;
        }
        public bool AntiTroll(Patient patient)
        {
            bool flag;
            flag = CheckBlockedStatus(patient);
            if (!flag)
            {
                return false;
            }

            int changed = 0, deleted = 0, created = 0;
            //Brojanje promena, brisanja i zakazivanja
            foreach (Requested req in _factory.RequestManager.Requested)
            {
                if (req.EmailPatient == patient.email)
                {
                    if (req.Operation == "1")
                    {
                        changed++;
                    } 
                    else if (req.Operation == "2")
                    {
                        deleted++;
                    } 
                    else if (req.Operation == "3"){ 
                        created++;
                    }
                }
            }
            if(changed > 4){
                Console.WriteLine("U prethodnih mesec ste promenili termin vise od 4 puta!");
                flag = false;
            }else if(created > 8)
            {
                Console.WriteLine("U proteklih mesec ste napravili vise od 8 zahteva!");
                flag = false;
            }else if(deleted >4)
            {
                Console.WriteLine("U protekih mesec ste otkazali vise od 4 puta!");
                flag = false;
            }
            if (!flag)
            {
                Console.WriteLine("Sorry, Your Account Has Been Blocked" +
                                  "Contact Support, Much Luck!");
                return false;
            }

            return true;
        }
        public void ShowAppointments(Patient patient)
        {
            int i = 1;
            Console.WriteLine("---------------------");
            Console.WriteLine("Current Appointments:");
            Console.WriteLine("---------------------");
            foreach (Appointment appointment in _factory.AppointmentManager.Appointment)
            {
                if (appointment.EmailPatient == patient.email)
                {
                    Console.Write("[" + i + "]");
                    appointment.PrintAppointment();
                    Console.WriteLine("---------------------");
                    i =+ 1;
                }
            }
        }

        public Doctor ResolveDoctorForAppointment(int autoFlag=0)
        {
            
            Doctor doctorForAppoint = new Doctor();
            if (autoFlag == 0)
            {
                int i = 1, inp;
                while (true)
                {
                    foreach (Doctor doctor in _factory.DoctorManager.Doctors)
                    {
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("[" + i + "]" + doctor);
                        Console.WriteLine("----------------------------------");
                        i += 1;
                    }

                    inp = Convert.ToInt32(Console.ReadLine());
                    if (inp > i || inp < 1)
                    {
                        Console.WriteLine("Invalid Input, try again");
                    }
                    else
                    {
                        break;
                    }
                }

                int j = 0;
                foreach (Doctor doctor in _factory.DoctorManager.Doctors)
                {
                    if (j == inp - 1) //
                    {
                        doctorForAppoint = doctor;
                    }

                }

                if (doctorForAppoint is null)
                {
                    doctorForAppoint = _factory.DoctorManager.Doctors[0];
                }
            }
            else
            {
                Random random = new Random(); 
                int i=0;
                foreach (Doctor doctor in _factory.DoctorManager.Doctors)
                {
                    i += 1;
                }

                int num = random.Next(0, i);
                int j = 0;
                foreach (Doctor doctor in _factory.DoctorManager.Doctors)
                {
                    if (j == num - 1) //
                    {
                        doctorForAppoint = doctor;
                    }

                }

                if (doctorForAppoint is null)
                {
                    doctorForAppoint = _factory.DoctorManager.Doctors[0];
                }
            }

            return doctorForAppoint;
        }

        public DateTime AutoTimeForAppointment(Doctor doctorForAppoint, DateTime timeDelta)
        {
            
            bool flag1=true, flag2=true, flag3=true;
            DateTime startTime = timeDelta.AddHours(-4);
            DateTime midTime = timeDelta;
            DateTime endTime = timeDelta.AddHours(4);
            while (true)
            {
                if (!_factory.DoctorManager.checkTime(startTime, startTime.AddMinutes(15),
                        doctorForAppoint) && flag1)
                {
                    startTime = startTime.AddMinutes(15);
                }
                else
                {
                    flag1 = false;
                }
                    
                if (!_factory.DoctorManager.checkTime(midTime, midTime.AddMinutes(15),
                        doctorForAppoint) && flag2)
                {
                    midTime = startTime.AddMinutes(15);
                }
                else
                {
                    flag2 = false;
                }
                if (!_factory.DoctorManager.checkTime(endTime, endTime.AddMinutes(15),
                        doctorForAppoint) && flag3)
                {
                    endTime = startTime.AddMinutes(15);
                }
                else
                {
                    flag3 = false;
                }

                if (flag1 == false && flag2 == false && flag3 == false)
                {
                    break;
                }
            }

            DateTime appointTime;
            List<DateTime> appointTimes = new List<DateTime>();
            appointTimes.Add(startTime);
            appointTimes.Add(midTime);
            appointTimes.Add(endTime);
            Console.WriteLine("Possible Times: ");
            int i = 1;
            foreach (DateTime time in appointTimes)
            {
                Console.WriteLine("[" + i + "] " + time);
                i++;
            }
            
            while (true)
            {
                int opt;
                Console.Write("Your Option: ");
                opt = Convert.ToInt32(Console.ReadLine());
                if (opt < 0 && opt > i)
                {
                    Console.WriteLine("Invalid Input, Try Again...");
                }
                else
                {
                    appointTime = appointTimes[opt - 1];
                    break;
                }
            }

            return appointTime;
        }
        
        public DateTime ResolveTimeForAppointment(Doctor doctorForAppoint)
        {
            DateTime appointTime;
            while (true)
            {
                appointTime = ShowDateTimeUserInput();
                DateTime currentDate = DateTime.Now;
                if (appointTime < currentDate.AddHours(12))
                {
                    Console.WriteLine("To Soon For Making an Appointment, try again.");
                }
                else if (!_factory.DoctorManager.checkTime(appointTime, appointTime.AddMinutes(15),
                             doctorForAppoint))
                {
                    Console.WriteLine("Doctor Will Be Busy At This Moment!");
                }
                else
                {
                    break;
                }
            }
            return appointTime;
        }

        public string ResolveRoomForAppointment(DateTime appointTime)
        {
            string roomId;
            while (true)
            {
                roomId = _factory.DoctorManager.CheckRoomOverview(appointTime, appointTime.AddMinutes(15));
                if (roomId != null)
                {
                    break;
                }
            }

            return roomId;
        }
        
        public void CreateAppointment(Patient patient)
        {
            string appType;
            Console.Write("Do You Wish To Manually Make Appointment?[Y/N]: ");
            appType = Console.ReadLine();
            if (appType == "N" || appType == "n")
            {
                AutoAppointment(patient);
            }
            else
            {
                Doctor doctorForAppoint;
                doctorForAppoint = ResolveDoctorForAppointment();
                

                DateTime appointTime;
                appointTime = ResolveTimeForAppointment(doctorForAppoint);

                string roomId;
                roomId = ResolveRoomForAppointment(appointTime);
                _factory.AppointmentManager.Appointment.Add(new Appointment(doctorForAppoint.email,
                    patient.email, appointTime, appointTime.AddMinutes(15), "OV", roomId, "0"));

                Requested requested = new Requested(patient.email, appointTime, appointTime.AddMinutes(15), "1");
                _factory.RequestManager.Serialize(requested);
            }
        }

        public void AutoAppointment(Patient patient)
        {
            Doctor doctorForAppoint = ResolveDoctorForAppointment(2);

            Console.WriteLine("Enter Date & Time, Time Delta Of 8 Hours Will Be Taken Into Consideration");
            DateTime timeDelta = ShowDateTimeUserInput();
            DateTime appointTime = AutoTimeForAppointment(doctorForAppoint, timeDelta);
            
            string roomId = ResolveRoomForAppointment(appointTime);
            
            // _factory.AppointmentManager.Appointment.Add(new Appointment(doctorForAppoint.email,
            //     patient.email, appointTime, appointTime.AddMinutes(15), "OV", roomId, "0"));
            //
            // Requested requested = new Requested(patient.email, appointTime, appointTime.AddMinutes(15), "1");
            // _factory.RequestManager.Serialize(requested);
        }
        public void UpdateAppointment(Patient patient)
            {
                string opt;   //1-cancel  2-change
                while (true)
                {
                    ShowAppointments(patient);
                    Console.WriteLine("Do You Wish To Change Time[1] || " +
                                      "Do You Wish To Cancel Appointment[2]\t(Else-Abort)");
                    opt = Console.ReadLine();
                    if (opt!= "1" || opt != "2")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input, try again!");
                    }
                }

                DateTime startTime, newTime;
                while (true)
                {

                    Console.WriteLine("\nEnter Date & Time Of Appoint " +
                                      "You Want To Change");
                    startTime = ShowDateTimeUserInput();
                    DateTime currentDate = DateTime.Now;
                    if (startTime < currentDate.AddDays(2))
                    {
                        Console.WriteLine("You Cannot Change This Appointment");
                        break;
                    }
                    else
                    {
                        if (opt != "2")
                        {
                            Console.WriteLine("\nEnter NEW Date & Time Of an Appoint ");
                            newTime = ShowDateTimeUserInput();
                        }
                        else
                        {
                            newTime = startTime;
                        }

                        Console.WriteLine("Filing request...");
                        Requested requested = new Requested(patient.email, startTime, newTime, opt);
                        _factory.RequestManager.Serialize(requested);
                    }
                }
            }

        public void SearchDoctors(){}
        public void ChangeNotifications(){}
        public void FillSurvey(){}

        public DateTime ShowDateTimeUserInput()
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