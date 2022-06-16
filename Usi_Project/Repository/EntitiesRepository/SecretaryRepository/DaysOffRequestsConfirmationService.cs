using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using Usi_Project.Appointments;
using Usi_Project.Users;


namespace Usi_Project.Repository
{
    public class DaysOffRequestsConfirmationService
    {
        public void printReq(DayOffRequest day)
        {
            Console.WriteLine("Start date: " + day.sdate);
            Console.WriteLine("End date: " + day.edate);
            Console.WriteLine("Reason: " + day.reason);
            Console.WriteLine("Emergency: " + day.emergency);
            Console.WriteLine("Verification: " + day.verification);
            Console.WriteLine("1) - Deny.");
            Console.WriteLine("2) - Confirm.");
        }

        public void DenyingReq(DayOffRequest day)
        {
            Console.WriteLine("Enter a reason for Denying: ");
            string reason = Console.ReadLine();
            day.verification = "1";
            Notification note = new Notification(day.sdate, day.edate, reason, "1", day.doctorEmail);
            SecretariesRepository._manager.NotificationRepository1.Notifications.Add(note);
        }

        public void ConfirmingReq(DayOffRequest day)
        {
            day.verification = "2";
            Notification note = new Notification(day.sdate, day.edate, "", "2", day.doctorEmail);
            SecretariesRepository._manager.NotificationRepository1.Notifications.Add(note);
        }
        
        public void DaysOffRequestsConfirmation()
        {
            
            foreach (var day in SecretariesRepository._manager.DayOffRepository.DaysOff)
            {
                printReq(day);
                if (day.verification == "0")
                {
                    string confirmation = Console.ReadLine();
                    if (confirmation == "1")
                    {
                        DenyingReq(day);
                    }
                    else if (confirmation == "2")
                    {
                        ConfirmingReq(day);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
            SecretariesRepository._manager.Saver.SaveDayOff(SecretariesRepository._manager.DayOffRepository.DaysOff);
            SecretariesRepository._manager.Saver.SaveNotification(SecretariesRepository._manager.NotificationRepository1
                .Notifications);

        }

        public void NotifyTheDoctor(Doctor doctor)
        {
            List<Notification> nonRemovableNoticifactions = new List<Notification>();
            foreach (var notification in SecretariesRepository._manager.NotificationRepository1.Notifications)
            {
                if (notification.doctorEmail == doctor.email)
                {
                    string confirmation;
                    if (notification.verification == "1")
                    {
                        confirmation = "Denied, reason being " + notification.reason;
                    }
                    else
                    {
                        confirmation = "Confirmed";
                    }
                    Console.WriteLine("Your Request for day off between dates " + notification.sdate + " and " 
                        + notification.edate + " has been " + confirmation);
                }
                else
                {
                    nonRemovableNoticifactions.Add(notification);
                }


            }
            SecretariesRepository._manager.Saver.SaveNotification(nonRemovableNoticifactions);
            
            
            
        }
    }
}