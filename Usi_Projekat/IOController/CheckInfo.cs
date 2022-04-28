using Usi_Projekat.Manage;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using Usi_Projekat.IOController;
using Usi_Projekat.Users;

namespace Usi_Projekat.IOController
{
    public class CheckInfo
    {
        private Factory _factory;

        public CheckInfo(Factory factory)
        {
            _factory = factory;
        }

        public void PrintMenu()
        {
            
            while (true)
                {
                    Console.WriteLine("Enter email: ");
                    string enteredEmail = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    string enteredPassword = Console.ReadLine();
                    Director director = _factory.DirectorManager.checkPersonalInfo(enteredEmail, enteredPassword);
                    if (director != null)
                    {
                       // director.Menu();
                        break;
                    }

                    else {
                        Doctor doctor = _factory.DoctorManager.checkPersonalInfo(enteredEmail, enteredPassword);
                        if (doctor != null)
                        {
                         //   doctor.Menu();
                            break;
                        }

                        else
                        {
                            Patient patient = _factory.PatientManager.checkPersonalInfo(enteredEmail, enteredPassword);
                            if (patient != null)
                            {
                             //   patient.Menu();
                                break;
                            }

                            else
                            {
                                Secretary secretary = _factory.SecretaryManager.checkPersonalInfo(enteredEmail, enteredPassword);
                                if (secretary != null)
                                {
                                    secretary.Menu(_factory);
                                    break;
                                }
                            }

                        }


                    }
                    Console.WriteLine("Not valid email/password combination, try again");
                }

        }
    }
}