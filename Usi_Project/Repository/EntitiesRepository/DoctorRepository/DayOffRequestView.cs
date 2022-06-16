using System;
using System.Collections.Generic;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.DoctorRepository
{
    public class DayOffRequestView
    {
        public DayOffRequestView()
        {
        }

        public DayOffRequest RequestForDayOff(Doctor doctor)
        {
            DayOffRequest dayOff;
            Console.WriteLine("Enter emergency: 1 for urgent  ");
            Console.WriteLine("Enter emergency: 0 for not urgent  ");
            string emergency = Console.ReadLine();
            Console.WriteLine("enter start date");
            DateTime sdate = ScheduleService.CreateDate();
            Console.WriteLine("enter end date");
            DateTime edate = ScheduleService.CreateDate();
            Console.WriteLine("Why do u need Day off? ");
            string reason = Console.ReadLine();
            if (emergency == "1")
            {
                while (true)
                {
                    if (ValidationService.CheckTimeForDaysOff(sdate))
                    {
                        dayOff = new DayOffRequest(doctor.email, sdate, edate, reason, emergency,"2");
                        return dayOff;
                    }
                        
                    else
                    {
                        Console.WriteLine("You cant request more than 5 days");
                        Console.WriteLine("enter start date");
                        sdate = ScheduleService.CreateDate();
                        Console.WriteLine("enter end date");
                        edate = ScheduleService.CreateDate();
                    }

                }
            }
            else
            {
                 dayOff = new DayOffRequest(doctor.email, sdate, edate, reason, emergency,"0");
                 return dayOff;

            }
            return dayOff;
        }

        public void DayOffRequests(List<DayOffRequest> daysOff, Doctor doctor)
        {
            foreach (var day in daysOff)
            {
                if (day.doctorEmail == doctor.email)
                {
                    Console.WriteLine(day.sdate);
                    Console.WriteLine(day.edate);
                    Console.WriteLine(day.reason);
                    Console.WriteLine(day.emergency);
                    Console.WriteLine(day.verification);
                }
                
            }
        }
    }
}