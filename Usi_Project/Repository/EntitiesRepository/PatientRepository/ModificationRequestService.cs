using System;
using Usi_Project.Appointments;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class ModificationRequestService
    {
        public static void UpdateAppointment(Patient patient)
            {
                string opt;   //1-cancel  2-change
                while (true)
                {
                    PatientAppointmentService.ShowAppointments(patient);
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
                    startTime = PatientManager.ShowDateTimeUserInput();
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
                            newTime = PatientManager.ShowDateTimeUserInput();
                        }
                        else
                        {
                            newTime = startTime;
                        }

                        Console.WriteLine("Filing request...");
                        Requested requested = new Requested(patient.email, startTime, newTime, opt);
                        PatientManager._factory.RequestManager.Serialize(requested);
                    }
                }
            }
    }
}