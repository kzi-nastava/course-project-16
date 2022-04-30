using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Channels;
using Usi_Project.Appointments;
using Usi_Project.Settings;


namespace Usi_Project.Manage
{
    public class DoctorManager
    {
        
        public List<Doctor> Doctors
        {
            get => _doctors;
            set => _doctors = value;
        }

        private string _doctorFilename;
        private List<Doctor> _doctors;
        private Factory _manager;
        private FileSettings file;

        public DoctorManager(string doctorFilename, Factory manager)
        {
            _doctorFilename = doctorFilename;
            _manager = manager;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(_doctorFilename), json);

        }

        public Doctor CheckPersonalInfo(string email, string password)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email && password == doctor.password)
                {
                    return doctor;
                }
            }
            return null;
        }
        
        public bool CheckEmail(string email)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email)
                {
                    return true;
                }
            }
            return false;
        }
         public void Menu(Doctor doctor)
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the appointment.");
            Console.WriteLine("2) - Schedule overview.");
            Console.WriteLine("3) - Update appointment.");
            Console.WriteLine("4) - Delete appointment");
            Console.WriteLine("5) - Cancel appointment");
            Console.WriteLine("6) - Overview or update medical record");
            Console.WriteLine("x) - Exit");
            Console.WriteLine("Choose: ");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    CreatingPatientAppointment(doctor);
                    break;
                
                case "2":
                    overviewSchedule(doctor);
                    break;
                
                case "3":
                    UpdateAppointments(doctor);
                    break;
                
                case "4":
                    DeleteAppointment(doctor);
                    break;
                
                case "5":
                    DateTime t = CreateDate();
                    CancelAppointment(t);
                    break;
                
                case "6":
                    Overview(doctor);
                    break;
                
                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu(doctor);
                    break;
            }
        }

        public bool checkTime(DateTime dateStart,DateTime dateEnd,Doctor doctor)
        {
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if ((dateStart>appointment.StartTime && dateStart<appointment.EndTime && doctor.email==appointment.EmailDoctor)||
                    (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && doctor.email==appointment.EmailDoctor))

                {
                    
                    return false;
                }
            }

            return true;
        }
        public bool CheckRoom(DateTime dateStart,DateTime dateEnd,string roomId)
        {
            
            foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && roomId==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && roomId==appointment.IdRoom))
                    {
                        return false;
                    }
                }

            return true;
        }
        
      

            
      

        public string CheckRoomOverview(DateTime dateStart,DateTime dateEnd)
        {
            int x = 0;
            foreach (var room in _manager.RoomManager.OverviewRooms)
            {
                foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && room.Id==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && room.Id==appointment.IdRoom))
                    {
                        x = 1;
                    }
                }

                if (x != 1)
                {
                    return room.Id;
                }

            }
            
            return null;
        }
        public string CheckOperation(DateTime dateStart,DateTime dateEnd)
        {
            int x = 0;
            foreach (var room in _manager.RoomManager.OperatingRooms)
            {
                foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && room.Id==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && room.Id==appointment.IdRoom))
                    {
                        x = 1;
                    }
                }

                if (x != 1)
                {
                    return room.Id;
                }

            }
            
            return null;
        }

        public bool CheckAnnualLeave()
        {
            return true;
        }

        public void overviewSchedule(Doctor doctor)
        {
            DateTime time = CreateDate();
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime >= time &&
                    appointment.EndTime <= time.AddDays(3))
                {
                    Console.WriteLine(appointment.StartTime);
                    appointment.PrintAppointment();

                }
                
            }
        }

        public void PrintDoctorsAppointments(Doctor doctor)
        {
            foreach (var app in _manager.AppointmentManager.Appointment)
            {
                if (app.EmailDoctor == doctor.email)
                {
                    app.PrintAppointment();
                }
                
            }
        }

        public void UpdateAppointments(Doctor doctor)
        {

            Console.WriteLine("Enter start time of appointment");
            var time = DateTime.Parse(Console.ReadLine());
            List<Appointment> appList = _manager.AppointmentManager.Appointment;
            foreach (var app in appList)

            {
                if (time == app.StartTime)
                {
                    while (true)
                    {
                        Console.WriteLine("1) - Update time.");
                        Console.WriteLine("2) - Update room.");
                        Console.WriteLine("3) - Update all.");
                        Console.WriteLine("x) - exit");
                        var chosenOption = Console.ReadLine();

                        switch (chosenOption)
                        {
                            case "1":
                                Console.WriteLine("Enter start time");
                                DateTime stime = CreateDate();
                                Console.WriteLine("Enter end time");
                                DateTime etime = CreateDate();
                                app.StartTime = stime;
                                app.EndTime = etime;
                                if (checkTime(stime, etime, doctor))
                                {
                                    _manager.Saver.SaveAppointment(appList);

                                }

                                break;

                            case "2":
                                Console.WriteLine("Enter room id");
                                var idroom = Console.ReadLine();
                                if (CheckRoom(app.StartTime, app.EndTime, idroom))
                                {
                                    app.IdRoom = idroom;
                                    _manager.Saver.SaveAppointment(appList);
                                }

                                break;

                            case "3":
                                Console.WriteLine("Enter start time");
                                DateTime stimes = CreateDate();
                                Console.WriteLine("Enter end time");
                                DateTime etimes = CreateDate();
                                app.StartTime = stimes;
                                app.EndTime = etimes;
                                if (checkTime(stimes, etimes, doctor))
                                {
                                    var idrooms = Console.ReadLine();
                                    if (CheckRoom(app.StartTime, app.EndTime, idrooms))
                                    {
                                        app.IdRoom = idrooms;
                                        app.StartTime = stimes;
                                        app.StartTime = etimes;
                                        _manager.Saver.SaveAppointment(appList);

                                    }
                                }

                                break;

                            case "x":
                                break;

                        }
                    }
                }
            }
        }



        public void DeleteAppointment(Doctor doctor)
        {
            PrintDoctorsAppointments(doctor);
            Console.WriteLine("Enter start time of appointment");
            DateTime time = CreateDate(); 
            List<Appointment> appointments = _manager.AppointmentManager.Appointment;
            foreach (var appointment in appointments)
            {
                if (appointment.StartTime == time)
                {
                    appointments.Remove(appointment);
                    break;
                }
                
            }
            
            _manager.Saver.SaveAppointment(appointments);

        }

        public DateTime CreateDate()
        {
            Console.WriteLine("Enter Month");
            int monthStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Day");
            int dayStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Hour");
            int hourStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Minute");
            int minuteStart = Convert.ToInt32(Console.ReadLine());
            var time = new DateTime(DateTime.Now.Year, monthStart, dayStart, hourStart,
                minuteStart, 0);
            return time;
        }
        
        public DateTime TodayAppointment()
        {
            Console.WriteLine("Enter Hour");
            int hourStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Minute");
            int minuteStart = Convert.ToInt32(Console.ReadLine());
            var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hourStart,
                minuteStart, 0);
            return time;
            
        }



        public void CreatingPatientAppointment(Doctor doctor)
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
            

            Console.WriteLine("Enter type ('OP' or 'OV')");
            string type = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("1) - Review of own overreviews");
                Console.WriteLine("2) - Make an appointment");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    overviewSchedule(doctor);
                }
                else
                {
                while (true)
                {
                    DateTime startTime = CreateDate();
                    if (type == "OV")
                    {
                        DateTime endTime = startTime.AddMinutes(15);
                        var idRoom = CheckRoomOverview(startTime, endTime);
                        if (checkTime(startTime, endTime, doctor))
                        {
                            if (idRoom != null)
                            {
                                List<Appointment> appointments = _manager.AppointmentManager.Appointment;
                                Appointment app= new Appointment(doctor.email,
                                    patientEmail,
                                    startTime, endTime, type, idRoom,"0");
                                appointments.Add(app);
                                _manager.Saver.SaveAppointment(appointments);
                                break;
                            }

                            Console.WriteLine("All rooms are busy in this term");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter duration of operation in minutes");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        DateTime endTime = startTime.AddMinutes(duration);

                        if (checkTime(startTime, endTime, doctor))
                        {
                            var idRoom = CheckRoomOverview(startTime, endTime);

                            if (idRoom != null)
                            {
                                List<Appointment> appointments = _manager.AppointmentManager.Appointment;
                                Appointment appointment= new Appointment(doctor.email,
                                    patientEmail,
                                    startTime, endTime, type, idRoom,"0");
                                _manager.AppointmentManager.Appointment.Add(appointment);
                               
                                _manager.Saver.SaveAppointment(appointments);
                                break;
                            }

                        }

                        Console.WriteLine("You fell into some term ");
                        overviewSchedule(doctor);

                    }
                }

                }

                break;
            }


        }

        public Appointment FindAppointment(DateTime time, Doctor doctor)
        {
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime == time)
                {
                    return appointment;
                }
                
            }

            return null;
        }




        public string GenerateRandomString()
        {

            int length = 7;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            System.Console.WriteLine(str_build.ToString());
            return str_build.ToString();
        }

        public void WriteAnamnesa(Appointment appointment)
        {
            Console.WriteLine("Napisite anamnesu");

            string anamnesaString = Console.ReadLine();
            string id = GenerateRandomString();
            Anamnesa anamnesa = new Anamnesa(id, appointment.EmailDoctor, appointment.EmailPatient, anamnesaString);
            List<Anamnesa> listAnamnesa = _manager.AnamnesaManager.Anamnesa;
            listAnamnesa.Add(anamnesa);
            _manager.Saver.SaveAnamnesa(listAnamnesa);
        }
        

        public void UpdateMedicalRecord(string email)
        {
            List<Patient> patients = _manager.PatientManager.Patients;
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
                    _manager.Saver.SavePatient(patients);
                    
                }
                
            }
            
            
        }

        public void UpdateStatus(DateTime time)
        {
            List<Appointment> list = _manager.AppointmentManager.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "1";
                    _manager.Saver.SaveAppointment(list);
                    break;
                }
            }
        }
        public void CancelAppointment(DateTime time)
        {
            List<Appointment> list = _manager.AppointmentManager.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "2";
                    _manager.Saver.SaveAppointment(list);
                    break;
                }
            }
        }

        public void Overview(Doctor doctor)
        {
            DateTime time= TodayAppointment();
            Appointment app=FindAppointment(time, doctor);
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Update medicalRecord.");
                Console.WriteLine("2) - Write anamnesa.");
                Console.WriteLine("x) - exit.");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        UpdateMedicalRecord(app.EmailPatient);
                        break;

                    case "2":
                        WriteAnamnesa(app);
                        UpdateStatus(app.StartTime);
                        break;

                    case "x":
                        break;
                }
                break;

            }
        }

    }
}
    
            
            






