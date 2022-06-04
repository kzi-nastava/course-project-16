using System;
using System.Collections.Generic;
using Usi_Project.DoctorFuncions;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class ResolveAppointmentDataService
    {
        
        public static Doctor ResolveDoctorForAppointment(int autoFlag=0)
        {
            
            Doctor doctorForAppoint = new Doctor();
            if (autoFlag == 0)
            {
                int i = 1, inp;
                while (true)
                {
                    foreach (Doctor doctor in PatientManager._factory.DoctorManager.Doctors)
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
                foreach (Doctor doctor in PatientManager._factory.DoctorManager.Doctors)
                {
                    if (j == inp - 1) //
                    {
                        doctorForAppoint = doctor;
                    }

                }

                if (doctorForAppoint is null)
                {
                    doctorForAppoint = PatientManager._factory.DoctorManager.Doctors[0];
                }
            }
            else
            {
                Random random = new Random(); 
                int i = PatientManager._factory.DoctorManager.Doctors.Count;


                int num = random.Next(0, i);
                int j = 0;
                foreach (Doctor doctor in PatientManager._factory.DoctorManager.Doctors)
                {
                    if (j == num - 1) //
                    {
                        doctorForAppoint = doctor;
                    }

                }

                if (doctorForAppoint is null)
                {
                    doctorForAppoint = PatientManager._factory.DoctorManager.Doctors[0];
                }
            }

            return doctorForAppoint;
        }

        public static DateTime AutoTimeForAppointment(Doctor doctorForAppoint, DateTime timeDelta)
        {
            
            bool flag1=true, flag2=true, flag3=true;
            DateTime startTime = timeDelta.AddHours(-4);
            DateTime midTime = timeDelta;
            DateTime endTime = timeDelta.AddHours(4);
            while (true)
            {
                if (!ValidationService.CheckTime(startTime, startTime.AddMinutes(15),
                        doctorForAppoint) && flag1)
                {
                    startTime = startTime.AddMinutes(15);
                }
                else
                {
                    flag1 = false;
                }
                    
                if (!ValidationService.CheckTime(startTime, startTime.AddMinutes(15),
                        doctorForAppoint) && flag2)
                {
                    midTime = startTime.AddMinutes(15);
                }
                else
                {
                    flag2 = false;
                }
                if (!ValidationService.CheckTime(startTime, startTime.AddMinutes(15),
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
        
        public static DateTime ResolveTimeForAppointment(Doctor doctorForAppoint)
        {
            DateTime appointTime;
            while (true)
            {
                appointTime = PatientManager.ShowDateTimeUserInput();
                DateTime currentDate = DateTime.Now;
                if (appointTime < currentDate.AddHours(12))
                {
                    Console.WriteLine("To Soon For Making an Appointment, try again.");
                }
                else if (!ValidationService.CheckTime(appointTime, appointTime.AddMinutes(15),
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

        public static string ResolveRoomForAppointment(DateTime appointTime)
        {
            string roomId;
            while (true)
            {
                roomId = ValidationService.GetIfFreeOverviewRoom(appointTime, appointTime.AddMinutes(15));
                if (roomId != null)
                {
                    break;
                }
            }

            return roomId;
        }
    }
}