using System;
using System.Collections.Generic;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class DoctorService
    {
        public static void SearchDoctors(Patient patient)
        {
            List<Doctor> docList = PatientManager._factory.DoctorManager.Doctors;
            List<Doctor> toSortList = new List<Doctor>();
            while (true)
            {
                string opt, param;
                Console.WriteLine("Search Parameter[else-Abort]");
                Console.WriteLine("[1] Name");
                Console.WriteLine("[2] Lastname");
                Console.WriteLine("[3] Specialisation");
                Console.WriteLine("{4} Grade");
                Console.Write("Your Choice: ");
                opt = Console.ReadLine();
                if (opt.ToLower() == "x")
                    break;
                if (opt == "1" || opt == "2" || opt == "3" || opt == "4")
                {
                    Console.WriteLine("Enter Parameter For Search: ");
                    param = Console.ReadLine();
                    int i = 1;
                    foreach (var doctor in docList)
                    {
                        if (opt == "1")
                        {
                            if (doctor.name == param)
                            {
                                Console.WriteLine("[" + i + "] " + doctor);
                                i += 1;
                                toSortList.Add(doctor);
                            }
                        }
                        else if (opt == "2")
                        {
                            if (doctor.lastName == param)
                            {
                                Console.WriteLine("[" + i + "] " + doctor);
                                i += 1;
                                toSortList.Add(doctor);
                            }
                        }
                        /*else if ()
                        {
                            if (doctor.specialisation == param)
                            {
                                Console.WriteLine("[" + i + "] " + doctor);
                                i += 1;
                            toSortList.Add(doctor);
                            }
                          } 
                          else{
                          Console.WriteLine("Needs Grading Implementation of 3.8")
                          } 
                        */
                    }
                    break;
                }
                Console.WriteLine("Invalid Input, Try Again...");
            }
            while (true)
            {
                string inp;
                Console.Write("Do You Wish To Sort Data[Y/N]: ");
                inp = Console.ReadLine();
                if (inp.ToLower() == "y")
                {
                    toSortList.Sort(delegate(Doctor x, Doctor y) { return x.email.CompareTo(y.email); });
                    foreach (var doc in toSortList)
                    {
                        Console.WriteLine(doc);
                    }
                }
                break;
            }
            string inp2;
            Console.Write("Do You Wish To Continue With Making Appointment[Y/N]: ");
            inp2 = Console.ReadLine();
            if (inp2.ToLower() == "y")
            {
                Console.Write("Enter Doctor Number: ");
                var inpDoc = Convert.ToInt32(Console.ReadLine()) - 1;
                PatientAppointmentService.CreateAppointment(patient, toSortList[inpDoc]);
            }
        }
    }
}
