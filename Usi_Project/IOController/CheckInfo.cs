using System;
using Usi_Project.DataSaver;
using Usi_Project.Repository;
using Usi_Project.Repository.EntitiesRepository.DirectorRepository;
using Usi_Project.Repository.EntitiesRepository.DirectorsRepository;
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
            _factory.RoomRepository.SaveData();
            _factory.DirectorRepository.SaveData();
            _factory.Saver.SaveDoctors(_factory.DoctorsRepository.Doctors);
            Environment.Exit(0);
            
        }

        public void PrintMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter email or x for exit: ");
                var enteredEmail = Console.ReadLine();
                if (enteredEmail == "x")
                    SaveData();
                Console.WriteLine("Enter password: ");
                string enteredPassword = Console.ReadLine();

                
                Director director = _factory.DirectorRepository.CheckPersonalInfo(enteredEmail, enteredPassword);
                if (director != null)
                {
                    DirectorService directorService = new DirectorService(_factory.DirectorRepository);
                    directorService.Menu();
                    continue;
                }
                Doctor doctor = _factory.DoctorsRepository.CheckPersonalInfo(enteredEmail,enteredPassword);
                if (doctor != null)
                {
                    _factory.DoctorsRepository.Menu(doctor);
                    continue;
                } 
                Patient patient =_factory.PatientsRepository.CheckPersonalInfo(enteredEmail, enteredPassword); 
                if (patient != null)
                {
                   _factory.PatientsRepository.Menu(patient);
                   continue;
                }
                Secretary secretary = _factory.SecretariesRepository.CheckPersonalInfo(enteredEmail, enteredPassword);
                if (secretary != null)
                {
                   SecretariesRepository.Menu();
                }
                else
                {
                   Console.WriteLine("Wrong username or password! Try again.");
                }
            }
        }
    }
}
