using System;
using Usi_Project.Repository;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;
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

        private void SaveData()
        {
            _factory.RoomManager.SaveData();
            _factory.DirectorManager.SaveData();
            Environment.Exit(0);
            
        }

        public void PrintMenu()
        {

            while (true)
            {
                while (true)
                {
                    while (true)
                    {
                        Console.WriteLine("Enter email or x for exit: ");
                        string enteredEmail = Console.ReadLine();
                        if (enteredEmail == "x")
                            SaveData();
                        Console.WriteLine("Enter password: ");
                        string enteredPassword = Console.ReadLine();
                        Director director = _factory.DirectorManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                        if (director != null)
                        {
                            DirectorManager.Menu();
                        }
                        
                        Doctor doctor =
                            _factory.DoctorManager.CheckPersonalInfo(enteredEmail,enteredPassword);
                        if (doctor != null)
                        {
                            _factory.DoctorManager.Menu(doctor);
                            break;
                        }
                        Patient patient =_factory.PatientManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                                       if (patient != null)
                                       {
                                           _factory.PatientManager.Menu(patient);
                                           break;
                                       }

                                       Secretary secretary =
                                           _factory.SecretaryManager.CheckPersonalInfo(enteredEmail,
                                               enteredPassword);
                                       if (secretary != null)
                                       {
                                           SecretaryManager.Menu();
                                           break;
                                       }
                                   
                    } 
                    Console.WriteLine("Not valid email/password combination, try again");
                }
            }
        }
    }
}
