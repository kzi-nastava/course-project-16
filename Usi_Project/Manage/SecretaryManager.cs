using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Specialized;
using Usi_Project.Users;
using Usi_Project.Appointments;

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

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
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
            Refresh();
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the patient profile.");
            Console.WriteLine("2) - Change the patient profile.");
            Console.WriteLine("3) - Delete the patient profile.");
            Console.WriteLine("4) - Block the patient profile.");
            Console.WriteLine("5) - Unblock the patient profile.");
            Console.WriteLine("6) - Confirmation of a request.");
            Console.WriteLine("7) - Schedule referal appointment");
            Console.WriteLine("8) - Emergency appointment.");
            Console.WriteLine("9) - Request dynamic equipment.");
            Console.WriteLine("10) - Deployment of dynamic equipment.");
            Console.WriteLine("x) - Exit.");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    DynamicEquipmentRequest();
                    
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
                    ConfirmationOfRequests();
                    break;
                case "7":
                    ReferalAppointment();
                    break;
                case "8":
                    EmergencyAppointment();
                    break;
                case "9":
                    DynamicEquipmentRequest();
                    break;
                case "10":
                    DeploymentOfDynamicEquipment();
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
                                patientAddress, patientPhone, 2, medicalRecord);
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
                if (patient.Blocked == 0)
                {
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
            else
            {
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
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                UnblockingPatientProfile();
            }
            else
            {
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

        void ConfirmationOfRequests()
        {
            List<Requested> forDeletion = new List<Requested>();
            List<Requested> forChanging = new List<Requested>();
            string inp;
            while (true)
            {
                Console.WriteLine("Do you wish to review requests for changes[1] or " +
                                  "requests for deletion[2](X-Exit)");
                inp = Console.ReadLine();
                if (inp == "x" || inp == "X")
                {
                    break;
                }
                else if (inp != "1" && inp != "2")
                {
                    Console.WriteLine("Invalid Input, try again...");
                }
                else
                {
                    break;
                }
            }

            switch (inp)
            {
                case "1":
                    foreach (Requested req in _manager.RequestManager.Requested)
                    {
                        if (req.Operation == "1")
                        {
                            Console.WriteLine("Patient: " + req.EmailPatient);
                            Console.WriteLine("Old Time: " + req.StartTime);
                            Console.WriteLine("New Time: " + req.NewTime);
                            while (true)
                            {
                                Console.Write("Approve[y/n]: ");
                                string opt = Console.ReadLine().ToLower();
                                if (opt == "y")
                                {
                                    forChanging.Add(req);
                                    break;
                                }
                            }
                        }
                    }
                    ResolveApproved(forChanging);
                    break;
                case "2":
                    foreach (Requested req in _manager.RequestManager.Requested)
                    {
                        if (req.Operation == "2")
                        {
                            Console.WriteLine("Patient: " + req.EmailPatient);
                            Console.WriteLine("Start Time: " + req.StartTime);
                            while (true)
                            {
                                Console.Write("Approve[y/n]: ");
                                string opt = Console.ReadLine().ToLower();
                                if (opt == "y")
                                {
                                    forDeletion.Add(req);
                                    break;
                                }
                            }
                        }
                    }
                    ResolveDeleted(forDeletion);
                    break;
                default:
                    Console.WriteLine("Aborting...");
                    break;
            }

        }

        // 0-Zakazan, 1-Odradjen, 2-Obrisan
        public void ResolveApproved(List<Requested> approved)
        {
            List<Appointment> appointmentList = _manager.AppointmentManager.Appointment;
            foreach (Requested request in approved)
            {
                foreach (Appointment appointment in appointmentList)
                {
                    if (request.EmailPatient == appointment.EmailPatient &&
                        request.StartTime == appointment.StartTime)
                    {
                        appointment.StartTime = request.NewTime;
                        appointment.EndTime = request.NewTime.AddMinutes(15);
                    }
                }
            }
        }

        // 0-Zakazan, 1-Odradjen, 2-Obrisan
        public void ResolveDeleted(List<Requested> deleted)
        {
            List<Appointment> appointmentList = _manager.AppointmentManager.Appointment;
            foreach (Requested request in _manager.RequestManager.Requested)
            {
                foreach (Appointment appointment in appointmentList)
                {
                    if (appointment.EmailPatient == request.EmailPatient &&
                        appointment.StartTime == request.StartTime)
                    {
                        appointment.Status = "2";
                    }
                }
            }
        }


        void ReferalOpAppointment(Doctor refferedDoctor, string patientEmail)
        {
            while (true)
            {
                DateTime startTime = _manager.DoctorManager.CreateDate();
                Console.WriteLine("Enter duration of operation in minutes");
                int duration = Convert.ToInt32(Console.ReadLine());
                DateTime endTime = startTime.AddMinutes(duration);

                if (_manager.DoctorManager.checkTime(startTime, endTime, refferedDoctor))
                {
                    var idRoom = _manager.DoctorManager.CheckRoomOverview(startTime, endTime);

                    if (idRoom != null)
                    {
                        List<Appointment> appointments = _manager.AppointmentManager.Appointment;
                        Appointment appointment = new Appointment(refferedDoctor.email,
                            patientEmail,
                            startTime, endTime, "OP", idRoom, "0");
                        _manager.AppointmentManager.Appointment.Add(appointment);

                        _manager.Saver.SaveAppointment(appointments);
                        break;
                    }
                }
                Console.WriteLine("You fell into some term ");
            }
        }
        

        void ReferalOvAppointment(Doctor refferedDoctor, string patientEmail)
        {
            while (true)
            {
                DateTime startTime = _manager.DoctorManager.CreateDate();
                DateTime endTime = startTime.AddMinutes(15);
                var idRoom = _manager.DoctorManager.CheckRoomOverview(startTime, endTime);
                if (_manager.DoctorManager.checkTime(startTime, endTime, refferedDoctor))
                {
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(refferedDoctor.email,
                            patientEmail,
                            startTime, endTime, "OV", idRoom, "0");
                        _manager.AppointmentManager.Appointment.Add(app);
                        _manager.Saver.SaveAppointment(_manager.AppointmentManager.Appointment);
                        break;
                    }

                    Console.WriteLine("All rooms are busy in this term");
                }
            }
        }

        
        public void DeleteReferal(string patientEmail)
        {
            for (int i = 0; i < _manager.PatientManager.Patients.Count; i++)
            {
                if (_manager.PatientManager.Patients[i].email == patientEmail)
                {
                    _manager.PatientManager.Patients[i].MedicalRecord.referral = null;
                }
            }
        }


        public string CheckEmailExist()
        {
            string patientEmail;
            while (true)
            {
                Console.WriteLine("Enter the Email of the patient:");
                patientEmail = Console.ReadLine();
                if (!_manager.PatientManager.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail doesn't exists, try again.");
                }
                else
                {
                    break;
                }
            }
            return patientEmail;
        }


        int PrintPatientsWithReferal()
        {
            int noReferals = 1;
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                if (patient.MedicalRecord.referral != null)
                {
                    noReferals = 0;
                    Console.WriteLine("Email: " + patient.email);
                }
            }

            return noReferals;
        }


        string FindDoctorForReferal(string patientEmail)
        {
            string doctorEmail = null;
            string doctorSpecialization = null;
            foreach (Patient patient in _manager.PatientManager.Patients)
            {
                if (patientEmail == patient.email)
                {
                    doctorEmail = patient.MedicalRecord.referral.DoctorEmail;
                    doctorSpecialization = patient.MedicalRecord.referral.DoctorSpecialisation;
                }
            }
            if (doctorEmail == null)
            {
                foreach (Doctor doctor in _manager.DoctorManager.Doctors)
                {
                    if (doctorSpecialization == doctor.Specialisation)
                    {
                        doctorEmail = doctor.email;
                    }
                }
            }

            return doctorEmail;
        }
        

       public void ReferalAppointment()
       {
           int noReferals = PrintPatientsWithReferal();
           if (noReferals == 1)
            {
                Console.WriteLine("No referals");
                Menu();
            }
            else
            {
                string patientEmail = CheckEmailExist();
                string doctorEmail = FindDoctorForReferal(patientEmail);
                if (doctorEmail == null)
                {
                    Console.WriteLine("Error, try again.");
                    ReferalAppointment();
                }
                else
                {
                    Doctor refferedDoctor = null;
                    foreach (Doctor doctor in _manager.DoctorManager.Doctors)
                    {
                        if (doctorEmail == doctor.email)
                        {
                            refferedDoctor = doctor;
                        }
                    }
                    Console.WriteLine("Enter type ('OP' or 'OV')");
                    string type = Console.ReadLine();
                    if (type == "OV")
                    {
                        ReferalOvAppointment(refferedDoctor, patientEmail);
                    }
                    else
                    {
                        ReferalOpAppointment(refferedDoctor, patientEmail);
                    }
                    DeleteReferal(patientEmail);
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

       
        public void PrintPatientsEmail()
       {
           foreach (Patient patient in _manager.PatientManager.Patients)
           {
               Console.WriteLine("Email: " + patient.email);
           }
       }

        
        public void EmergencyAppointment()
        {
            PrintPatientsEmail();
            Console.WriteLine("Enter the email (x for exit) of a patient that  you want to schedule appointment for:");
            string enteredEmail = Console.ReadLine();
            if (enteredEmail == ("x"))
            {
                Menu();
            }
            else if (!_manager.PatientManager.CheckEmail(enteredEmail))
            {
                Console.WriteLine("Invalid email entered, try again");
                EmergencyAppointment();
            }
            else
            ChooseSpecialization(enteredEmail);
        }
        
        
        public void ChooseSpecialization(string enteredEmail)
        {
            Console.WriteLine("Chose the Doctor specialization");
            Console.WriteLine("1) - Cardiology");
            Console.WriteLine("x) - exit");
            string chosenSpecialization = Console.ReadLine();
            switch (chosenSpecialization)
            {
                case "1":
                    EmergencyAppointmentSpecializationBased("Cardiology", enteredEmail);
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    EmergencyAppointment();
                    break;
            }
            Menu();
        }
        
        
        void EmergencyAppointmentSpecializationBased(string specialization, string enteredEmail)
        {
            Console.WriteLine("Enter the duration of a Appointment: ");
            int appointmentDuration = Convert.ToInt32(Console.ReadLine());
            foreach (Doctor doctor in _manager.DoctorManager.Doctors)
            {
                if (specialization == doctor.Specialisation)
                {
                    DateTime appointTime = DateTime.Now;
                    int appointmentFound = FreeTermFound(doctor, appointTime, appointmentDuration, enteredEmail);
                    if (appointmentFound == 0)
                    {
                        RescheduleAppointment(doctor, appointTime, enteredEmail);
                    }
                    break;
                }
            }
        }
        
        
        

        
        int FreeTermFound(Doctor doctor, DateTime appointTime, int appointmentDuration, string enteredEmail)
        {
           int appointmentFound = 0;
            for (int i = 0; i < 120; i += 10)
            {
                if (_manager.DoctorManager.checkTime(appointTime.AddMinutes(i),
                        appointTime.AddMinutes(i + appointmentDuration), doctor))
                {
                    var idRoom = _manager.DoctorManager.CheckRoomOverview(appointTime.AddMinutes(i),
                        appointTime.AddMinutes(i + appointmentDuration));
                    if (idRoom != null)
                    {
                        Appointment appointment = new Appointment(doctor.email,
                            enteredEmail,
                            appointTime.AddMinutes(i), appointTime.AddMinutes(i + appointmentDuration),
                            "OP", idRoom, "0");
                        _manager.AppointmentManager.Appointment.Add(appointment);
                        appointmentFound = 1;
                        _manager.Saver.SaveAppointment(_manager.AppointmentManager.Appointment);
                        break;
                    }
                }
            }

            return appointmentFound;
        }
        
        
        public void RescheduleAppointment(Doctor doctor, DateTime appointTime, string enteredEmail)
        {
            List<Appointment> scheduledAppointments = FindScheduledAppointments(doctor, appointTime);
            int indexOfAppointment = ChooseTermToReschedule(scheduledAppointments);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (i == indexOfAppointment)
                {
                    if (scheduledAppointments[i].Type == "OV")
                    {
                        ScheduleTypeOvAppointment(doctor, scheduledAppointments, i);
                    }
                    else
                    {
                        ScheduleTypeOpAppointment(doctor, scheduledAppointments, i);
                    }
                    ScheduleEmergencyAppointment(doctor, enteredEmail, scheduledAppointments, i);
                }
            }
            
        }

        
        List<Appointment> FindScheduledAppointments(Doctor doctor, DateTime appointTime)
        {
            List<Appointment> scheduledAppointments = new List<Appointment>();
            foreach (Appointment appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email &&
                    appointment.StartTime > appointTime &&
                    appointment.StartTime < appointTime.AddHours(2))
                {
                    scheduledAppointments.Add(appointment);
                }

            }
            return scheduledAppointments;
        }
        
        
        int ChooseTermToReschedule(List<Appointment>scheduledAppointments)
        {
            int indexOfAppointment;
            while (true)
            {
                Console.WriteLine("Enter the appointment you want to reschedule");
                for (int i = 0; i < scheduledAppointments.Count; i++)
                {
                    Console.WriteLine(i + ")" + "Start time: " + scheduledAppointments[i].StartTime
                                      + " end time: " + scheduledAppointments[i].EndTime);
                }

                 indexOfAppointment = Convert.ToInt32(Console.ReadLine());
                if (indexOfAppointment < 0 || indexOfAppointment > scheduledAppointments.Count)
                {
                    Console.WriteLine("Invalid option, try again.");
                }
                else
                {
                    break;
                }
            }
            return indexOfAppointment;
        }

        void ScheduleTypeOvAppointment(Doctor doctor, List<Appointment> scheduledAppointments, int i)
        {
            while (true)
            {
                DateTime startTime = _manager.DoctorManager.CreateDate();
                DateTime endTime = startTime.AddMinutes(15);
                var idRoom =
                    _manager.DoctorManager.CheckRoomOverview(startTime, endTime);
                if (_manager.DoctorManager.checkTime(startTime, endTime, doctor))
                {
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(doctor.email,
                            scheduledAppointments[i].EmailPatient,
                            startTime, endTime, "OV", idRoom, "0");
                        _manager.AppointmentManager.Appointment.Add(app);
                        _manager.Saver.SaveAppointment(_manager.AppointmentManager.Appointment);
                        break;
                    }

                    Console.WriteLine("All rooms are busy in this term");
                }
            }
        }

        void ScheduleTypeOpAppointment(Doctor doctor, List<Appointment> scheduledAppointments, int i)
        {
            while (true)
            {
                DateTime startTime = _manager.DoctorManager.CreateDate();
                Console.WriteLine("Enter duration of operation in minutes");
                int duration = Convert.ToInt32(Console.ReadLine());
                DateTime endTime = startTime.AddMinutes(duration);

                if (_manager.DoctorManager.checkTime(startTime, endTime, doctor))
                {
                    var idRoom = _manager.DoctorManager.CheckRoomOverview(startTime, endTime);
                    if (idRoom != null)
                    {
                        Appointment app = new Appointment(doctor.email,
                            scheduledAppointments[i].EmailPatient,
                            startTime, endTime, "OP", idRoom, "0");
                        _manager.AppointmentManager.Appointment.Add(app);
                        _manager.Saver.SaveAppointment(_manager.AppointmentManager.Appointment);
                        break;
                    }
                }

                Console.WriteLine("You fell into some term ");
            }
        }

        void ScheduleEmergencyAppointment(Doctor doctor, string enteredEmail, List<Appointment> scheduledAppointments,
            int i)
        {
            Appointment appointment = new Appointment(doctor.email,
                enteredEmail,
                scheduledAppointments[i].StartTime,
                scheduledAppointments[i].EndTime, scheduledAppointments[i].Type,
                scheduledAppointments[i].IdRoom, "0");
            _manager.AppointmentManager.Appointment.Add(appointment);
            _manager.Saver.SaveAppointment(_manager.AppointmentManager.Appointment);
        }

        void PrintAllOutOfStockDynamicTool()
        {
            Dictionary<DynamicEquipment, int> nekaList = _manager.RoomManager.StockRoom.DynamicEquipment;
            foreach (OperatingRoom operatingRoom in _manager.RoomManager.OperatingRooms)
            {
                nekaList = operatingRoom.PrintDynamicTools(nekaList);
            }
            foreach (OverviewRoom overviewRoom in _manager.RoomManager.OverviewRooms)
            {
                nekaList = overviewRoom.PrintDynamicTools(nekaList);
            }
            
            foreach (var liste in nekaList)
            {
                if (liste.Value == 0)
                {
                    Console.WriteLine(liste.Key + " out of stock");
                }
            }
        }

        void DynamicEquipmentRequest()
        {
            PrintAllOutOfStockDynamicTool();
            while (true)
            {
                int validEntry = 0;
                Dictionary<DynamicEquipment, int> nekaList2 = new Dictionary<DynamicEquipment, int>();
                Console.WriteLine("Enter Dynamic tool(Gauze, Buckles, Bandages, Paper, Pencils) you want to create a request for (x for exit): ");
                string toolName = Console.ReadLine().ToUpper();
                int numberOfTools; 
                switch (toolName)
                {
                    case "GAUZE":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "BUCKLES":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "BANDAGES":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "PAPER":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, numberOfTools);
                        nekaList2.Add(DynamicEquipment.Pencils, 0);
                        validEntry = 1;
                        break;
                    case "PENCILS":
                        Console.WriteLine("Enter how much do you want to order: ");
                        numberOfTools = Convert.ToInt32(Console.ReadLine());
                        nekaList2.Add(DynamicEquipment.Gauze, 0);
                        nekaList2.Add(DynamicEquipment.Buckles, 0);
                        nekaList2.Add(DynamicEquipment.Bandages, 0);
                        nekaList2.Add(DynamicEquipment.Paper, 0);
                        nekaList2.Add(DynamicEquipment.Pencils, numberOfTools);
                        validEntry = 1;
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("You didnt enter valid name, try again.");
                        break;
                }
                if (toolName == "X")
                {
                    break;
                }
                if (validEntry == 0)
                {
                }
                else
                {
                    DateTime currentTime = DateTime.Now;
                    DynamicRequest rikvest = new DynamicRequest(currentTime.AddHours(24), nekaList2);
                    _manager.DynamicRequestManager.DynamicRequests.Add(rikvest);
                    _manager.Saver.SaveDynamicRequest(_manager.DynamicRequestManager.DynamicRequests);
                    break;
                }
            }
            Menu();
        }

        void Refresh()
        {
            DateTime currentTime = DateTime.Now;
            for (int i = 0; i < _manager.DynamicRequestManager.DynamicRequests.Count; i++)
            {
                if (currentTime > _manager.DynamicRequestManager.DynamicRequests[i].TimeOfAddition)
                {
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Gauze] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Gauze];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Buckles] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Buckles];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Bandages] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Bandages];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Paper] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Paper];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Pencils] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Pencils];
                    _manager.DynamicRequestManager.DynamicRequests.Remove(_manager.DynamicRequestManager.DynamicRequests[i]);
                }
            }

            using (StreamWriter file = File.CreateText(_manager.DynamicRequestManager.RequestFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _manager.DynamicRequestManager.DynamicRequests);
            }
            _manager.RoomManager.SaveData();
        }


        public List<string> PrintLowSupplyEquipmentFromAllRooms()
        {
            List<string> roomsId = new List<string>();
            foreach (OperatingRoom operatingRoom in _manager.RoomManager.OperatingRooms)
            {
                roomsId =  operatingRoom.PrintLowSupplyEquipment(roomsId);
            }
            foreach (OverviewRoom overviewRoom in _manager.RoomManager.OverviewRooms)
            {
                roomsId = overviewRoom.PrintLowSupplyEquipment(roomsId);
            }
            roomsId = _manager.RoomManager.StockRoom.PrintLowSupplyEquipment(roomsId);
            return roomsId;
        }

        
        public string GetReceivingRoomid(List<string> roomsId)
        {
            string receivingRoomId = "";
            while (true)
            {
                Console.WriteLine("Enter id of the room that you want to add equipment to: ");
                receivingRoomId = Console.ReadLine();
                if (roomsId.Contains(receivingRoomId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid id entered, try again.");
                }
            }
            return receivingRoomId;
        }
        
        
        public string GetSupplyingRoomid(List<string> roomsId)
        {
            string supplyingRoomId = "";
            while (true)
            {
                Console.WriteLine("Enter id of the room that you want to take equipment from: ");
                supplyingRoomId = Console.ReadLine();
                if (roomsId.Contains(supplyingRoomId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid id entered, try again.");
                }
            }
            return supplyingRoomId;
        }

        public int ValidateInput()
        {
            int chosenDynamicEq;
            while (true)
            {
                chosenDynamicEq = Convert.ToInt32(Console.ReadLine());
                if (chosenDynamicEq > 4 || chosenDynamicEq < 0)
                {
                    Console.WriteLine("Wrong option entered, try again: ");
                }
                else
                {
                    break;
                }
            }

            return chosenDynamicEq;
        }
        void DeploymentOfDynamicEquipment()
        {
            List<string> roomsId = PrintLowSupplyEquipmentFromAllRooms();
            string receivingRoomId = GetReceivingRoomid(roomsId);
            string supplyingRoomId = GetSupplyingRoomid(roomsId);
            Console.WriteLine("Enter Dynamic tool you want to move (x for exit): ");
            int numberOfTools;
            Dictionary<int, DynamicEquipment> dictionary = new Dictionary<int, DynamicEquipment>();
            int i = -1;
            foreach (DynamicEquipment eq in Enum.GetValues(typeof(DynamicEquipment)))
            {
                i++;
                dictionary[i] = eq;
                Console.WriteLine( i + ")" + " " + dictionary[i]);
            }
            int chosenDynamicEq = ValidateInput();
            DynamicEquipment chosen = dictionary[chosenDynamicEq];
            Console.WriteLine("Enter how much do you want to move: ");
            numberOfTools = Convert.ToInt32(Console.ReadLine());
            foreach (OverviewRoom receivingRoom in _manager.RoomManager.OverviewRooms)
                GiveEqToOvRoom(receivingRoomId, numberOfTools, supplyingRoomId, receivingRoom, chosen);
            foreach (OperatingRoom receivingRoom in _manager.RoomManager.OperatingRooms)
                GiveEqToOpRoom(receivingRoomId, numberOfTools, supplyingRoomId, receivingRoom, chosen);
            GiveEqToStock(receivingRoomId, numberOfTools, supplyingRoomId, _manager.RoomManager.StockRoom,
                        chosen);
            _manager.RoomManager.SaveData();
            Menu();
        }

        OverviewRoom CheckIfIdInOVRoom(string supplyingRoomId)
        {
            foreach (OverviewRoom supplyingRoom in _manager.RoomManager.OverviewRooms)
            {
                if (supplyingRoom.Id == supplyingRoomId)
                {
                    return supplyingRoom;
                }
            }
            return null;
        }
        
        OperatingRoom CheckIfIdInOPRoom(string supplyingRoomId)
        {
            foreach (OperatingRoom supplyingRoom in _manager.RoomManager.OperatingRooms)
            {
                if (supplyingRoom.Id == supplyingRoomId)
                {
                    return supplyingRoom;
                }
            }
            return null;
        }


        public void TakeEqFromOpGiveToOv(int numberOfTools, OperatingRoom supplyingRoom1, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }


        public void TakeEqFromOvGiveToOv(int numberOfTools, OverviewRoom supplyingRoom1, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromStockGiveToOv(string supplyingRoomId, int numberOfTools, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (_manager.RoomManager.StockRoom.Id == supplyingRoomId)
            {
                if (_manager.RoomManager.StockRoom.DynamicEquipment[chosen] >= numberOfTools)
                {
                    _manager.RoomManager.StockRoom.DynamicEquipment[chosen] -= numberOfTools;
                    receivingRoom.DynamicEquipment[chosen] += numberOfTools;
                }
                else
                {
                    Console.WriteLine("Not enough equipment in chosen room.");
                }
            }
        }
        

        public void GiveEqToOvRoom(string receivingRoomId, int numberOfTools, string supplyingRoomId, OverviewRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                TakeEqFromStockGiveToOv(supplyingRoomId, numberOfTools, receivingRoom, chosen);
                OperatingRoom supplyingRoom = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOpGiveToOv(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }
                OverviewRoom supplyingRoom1 = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOvGiveToOv(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }


        public void TakeEqFromOvGiveToOp(int numberOfTools, OverviewRoom supplyingRoom1, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromOpGiveToOp(int numberOfTools, OperatingRoom supplyingRoom1, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }


        public void TakeEqFromStockGiveToOp(string supplyingRoomId, int numberOfTools, OperatingRoom receivingRoom, DynamicEquipment chosen )
        {
            if (_manager.RoomManager.StockRoom.Id == supplyingRoomId)
            {
                if (_manager.RoomManager.StockRoom.DynamicEquipment[chosen] >= numberOfTools)
                {
                    _manager.RoomManager.StockRoom.DynamicEquipment[chosen] -= numberOfTools;
                    receivingRoom.DynamicEquipment[chosen] += numberOfTools;
                }
                else
                {
                    Console.WriteLine("Not enough equipment in chosen room.");
                    
                }
            }
        }
        
        
        public void GiveEqToOpRoom(string receivingRoomId, int numberOfTools, string supplyingRoomId, OperatingRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                TakeEqFromStockGiveToOp(supplyingRoomId, numberOfTools, receivingRoom, chosen);
                OverviewRoom supplyingRoom = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOvGiveToOp(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }

                OperatingRoom supplyingRoom1 = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOpGiveToOp(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }
        
        
        public void TakeEqFromOvGiveToStock(int numberOfTools, OverviewRoom supplyingRoom1, StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
            }
        }
        
        
        public void TakeEqFromOpGiveToStock(int numberOfTools, OperatingRoom supplyingRoom1, StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (supplyingRoom1.DynamicEquipment[chosen] >= numberOfTools)
            {
                supplyingRoom1.DynamicEquipment[chosen] -= numberOfTools;
                receivingRoom.DynamicEquipment[chosen] += numberOfTools;
            
            }
            else
            {
                Console.WriteLine("Not enough equipment in chosen room.");
               
            }
        }

        
        public void GiveEqToStock(string receivingRoomId, int numberOfTools, string supplyingRoomId,
            StockRoom receivingRoom, DynamicEquipment chosen)
        {
            if (receivingRoom.Id == receivingRoomId)
            {
                OverviewRoom supplyingRoom = CheckIfIdInOVRoom(supplyingRoomId);
                if (supplyingRoom != null)
                {
                    TakeEqFromOvGiveToStock(numberOfTools, supplyingRoom, receivingRoom, chosen);
                }
                OperatingRoom supplyingRoom1 = CheckIfIdInOPRoom(supplyingRoomId);
                if (supplyingRoom1 != null)
                {
                    TakeEqFromOpGiveToStock(numberOfTools, supplyingRoom1, receivingRoom, chosen);
                }
            }
        }
    }
}