using System;
using System.Collections.Generic;
using Usi_Project.Appointments;
using Usi_Project.Manage;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class ScheduleService
    {
        public Doctor doctor;
        public Factory _doctorScheduleManager;
        public ValidationService _validation;

        public ScheduleService(Doctor doctor, Factory doctorScheduleManager, ValidationService validation)
        {
            this.doctor = doctor;
            _doctorScheduleManager = doctorScheduleManager;
            _validation = validation;
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
        public void OverviewSchedule(Doctor doctor)
        {
            DateTime time = CreateDate();
            foreach (var appointment in _doctorScheduleManager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime >= time &&
                    appointment.EndTime <= time.AddDays(3))
                {
                    Console.WriteLine(appointment.StartTime);
                    appointment.PrintAppointment();

                }
            }
        }
        public void CreatingPatientAppointment(Doctor doctor)
        {
            string patientEmail = _validation.GetValidPatientEmail();
            while (true)
            {
                Console.WriteLine("Enter type ('OP' or 'OV')");
                string type = Console.ReadLine();
                if (type == "OV")
                {
                    CreateOverviewAppointment(patientEmail,doctor);
                    return;
                }
                if(type =="OP")
                {
                    CreateOperationAppointment(patientEmail,doctor);
                    return;
                }
                Console.WriteLine("That type of appointment doesnt exist");
            }
        } 
        public void CreateOverviewAppointment(string patientEmail,Doctor doctor)
        {
            DateTime startTime = CreateDate();
            var idRoom = _validation.GetIfFreeOverviewRoom(startTime, startTime.AddMinutes(15));
            if (idRoom == null)
            {
                Console.WriteLine("All rooms are busy in this term");
            }
            else
            {
                if (_validation.CheckTime(startTime, startTime.AddMinutes(15), doctor))
                {
                    List<Appointment> appointments = _doctorScheduleManager.AppointmentManager.Appointment;
                    Appointment app= new Appointment(doctor.email, patientEmail, startTime,startTime.AddMinutes(15) , "OV", idRoom,"0");
                    appointments.Add(app);
                    _doctorScheduleManager.Saver.SaveAppointment(appointments);
                
                } 
            }
        } 
        public void CreateOperationAppointment(string patientEmail,Doctor doctor)
        {
            DateTime startTime = CreateDate();
            DateTime endTime = CreateDate();
            var idRoom = _validation.GetIfFreeOperatingRoom(startTime, endTime);
            if (idRoom == null)
            {
                Console.WriteLine("All rooms are busy in this term");
            }
            else
            {
                if (_validation.CheckTime(startTime, endTime, doctor))
                {
                    List<Appointment> appointments = _doctorScheduleManager.AppointmentManager.Appointment;
                    Appointment app= new Appointment(doctor.email, patientEmail, startTime,endTime , "OP", idRoom,"0");
                    appointments.Add(app);
                    _doctorScheduleManager.Saver.SaveAppointment(appointments);
                
                } 
            }
        } 
        public void UpdateAppointmentTime(Appointment app,List<Appointment>appList)
        {
            Console.WriteLine("Enter start time");
            DateTime stime = CreateDate();
            Console.WriteLine("Enter end time");
            DateTime etime = CreateDate();
            app.StartTime = stime;
            app.EndTime = etime;
            if (_validation.CheckTime(stime, etime, doctor))
            {
                _doctorScheduleManager.Saver.SaveAppointment(appList);
            }
        } 
        public void UpdateAppointmentRoom(Appointment app,List<Appointment>appList)
        {
            Console.WriteLine("Enter room id");
            var idroom = Console.ReadLine();
            if (_validation.CheckRoom(app.StartTime, app.EndTime, idroom))
            {
                app.IdRoom = idroom;
                _doctorScheduleManager.Saver.SaveAppointment(appList);
            }
        }
        public void UpdateAppointments(Doctor doctor)
        {
            Console.WriteLine("Enter start time of appointment");
            var time = DateTime.Parse(Console.ReadLine());
            List<Appointment> appList = _doctorScheduleManager.AppointmentManager.Appointment;
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
                                UpdateAppointmentTime(app,appList);
                                break;

                            case "2":
                                UpdateAppointmentRoom(app,appList);
                                break;

                            case "3":
                                UpdateAppointmentTime(app,appList);
                                UpdateAppointmentRoom(app,appList);
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
            _doctorScheduleManager.DoctorManager.PrintDoctorsAppointments(doctor);
            Console.WriteLine("Enter start time of appointment");
            DateTime time = CreateDate();
            List<Appointment> appointments = _doctorScheduleManager.AppointmentManager.Appointment;
            foreach (var appointment in appointments)
            {
                if (appointment.StartTime == time)
                {
                    appointments.Remove(appointment);
                    break;
                }
            }
            _doctorScheduleManager.Saver.SaveAppointment(appointments);
        }
        public void CancelAppointment(DateTime time)
        {
            List<Appointment> list = _doctorScheduleManager.AppointmentManager.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "2";
                    _doctorScheduleManager.Saver.SaveAppointment(list);
                    break;
                }
            }
        }
    }
}






