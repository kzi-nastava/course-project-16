using System;
using Usi_Project.Manage;
using Usi_Project.Users;

namespace Usi_Project.IOController
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

                while (true)
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
                            DirectorManager.Menu();
                            break;
                        }

                        else
                        {
                            Doctor doctor = _factory.DoctorManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                            if (doctor != null)
                            {
                                //DoctorManager.Menu();
                                break;
                            }

                            else
                            {
                                Patient patient =
                                    _factory.PatientManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                                if (patient != null)
                                {
                                    //patient.Menu();
                                    break;
                                }

                                else
                                {
                                    Secretary secretary =
                                        _factory.SecretaryManager.CheckPersonalInfo(enteredEmail, enteredPassword);
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
    }
}