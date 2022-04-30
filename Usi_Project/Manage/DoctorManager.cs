using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;
using Usi_Project.Appointments;

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
                Console.WriteLine(email);
                Console.WriteLine(password);
                Console.WriteLine(doctor.email);
                Console.WriteLine(doctor.password);
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
            Console.WriteLine("1) - Create the apointment.");
            Console.WriteLine("2) - Schedule overview.");
            Console.WriteLine("3) - Perform patient examinations.");
            Console.WriteLine("4) - ");
            Console.WriteLine("5) - ");
            Console.WriteLine("6) - ");
            Console.WriteLine("x) - Exit");
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
                    //DeletingPatientProfile();
                    break;
                
                case "4":
                  //  BlockingPatientProfile();
                    break;
                
                case "5":
                   // UnblockingPatientProfile();
                    break;
                
                case "6":
                    //ConfirmationOfRequests();
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
                    Console.WriteLine(dateStart);
                    Console.WriteLine(dateEnd);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(appointment.StartTime);
                    Console.WriteLine(appointment.EndTime);
                    Console.WriteLine("------------------------------");
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
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime >= DateTime.Now &&
                    appointment.EndTime < DateTime.Now.AddDays(3))
                {
                    Console.WriteLine(appointment.ToString());
                    
                }
                
            }
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
                Console.WriteLine("1) - Pregled sopstvenih pregleda");
                Console.WriteLine("2) - Zakazivanje direktno");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    overviewSchedule(doctor);
                }
                else
                {
                while (true)
                {
                    Console.WriteLine("Enter Month");
                    int monthStart = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Day");
                    int dayStart = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Hour");
                    int hourStart = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Minute");
                    int minuteStart = Convert.ToInt32(Console.ReadLine());
                    DateTime startTime = new DateTime(DateTime.Now.Year, monthStart, dayStart, hourStart,
                        minuteStart, 0);
                    if (type == "OV")
                    {
                        DateTime endTime = startTime.AddMinutes(15);
                        Console.WriteLine("JEBISE");
                        var idRoom = CheckRoomOverview(startTime, endTime);
                        if (checkTime(startTime, endTime, doctor))
                        {
                            if (idRoom != null)
                            {
                                Console.WriteLine("JEBISE111111");
                                _manager.AppointmentManager.Appointment.Add(new Appointment(doctor.email,
                                    patientEmail,
                                    startTime, endTime, type, idRoom));
                                break;
                            }

                            Console.WriteLine("All rooms are busy in this term");
                        }
                    }
                    else
                    {
                        Console.WriteLine("JEBISE1");
                        Console.WriteLine("Enter duration of operation in minutes");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        DateTime endTime = startTime.AddMinutes(duration);

                        if (checkTime(startTime, endTime, doctor))
                        {
                            var idRoom = CheckRoomOverview(startTime, endTime);

                            if (idRoom != null)
                            {
                                _manager.AppointmentManager.Appointment.Add(new Appointment(doctor.email,
                                    patientEmail,
                                    startTime, endTime, type, idRoom));
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

    }
}



