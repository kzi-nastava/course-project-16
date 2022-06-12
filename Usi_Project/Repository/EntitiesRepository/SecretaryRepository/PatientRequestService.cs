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
    public class PatientRequestService
    {
        public void ConfirmationOfRequests()
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
                    foreach (Requested req in SecretaryManager._manager.RequestManager.Requested)
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
                    foreach (Requested req in SecretaryManager._manager.RequestManager.Requested)
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
            List<Appointment> appointmentList = SecretaryManager._manager.AppointmentManager.Appointment;
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
            List<Appointment> appointmentList = SecretaryManager._manager.AppointmentManager.Appointment;
            foreach (Requested request in SecretaryManager._manager.RequestManager.Requested)
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
        
    }
}