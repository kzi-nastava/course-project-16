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
                    Director director = _factory.DirectorManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                    if (director != null)
                    {
                       // director.Menu();
                        break;
                    }

                    else {
                        Doctor doctor = _factory.DoctorManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                        if (doctor != null)
                        {
                         //   doctor.Menu();
                            break;
                        }

                        else
                        {
                            Patient patient = _factory.PatientManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                            if (patient != null)
                            {
                             //   patient.Menu();
                                break;
                            }

                            else
                            {
                                Secretary secretary = _factory.SecretaryManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                                if (secretary != null)
                                {
                                    _factory.SecretaryManager.Menu();
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