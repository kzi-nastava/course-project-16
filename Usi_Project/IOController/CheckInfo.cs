using System;
using Usi_Project.DataSaver;
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
            _factory.Saver.SaveDoctors(_factory.DoctorManager.Doctors);
            Environment.Exit(0);
            
        }

        public void PrintMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter email or x for exit: ");
                var enteredEmail = "MilosK@gmail.com";//Console.ReadLine();
                if (enteredEmail == "x")
                    SaveData();
                Console.WriteLine("Enter password: ");
                string enteredPassword = "Miki123";//Console.ReadLine();

                Director director = _factory.DirectorManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                if (director != null)
                {
                    DirectorManager.Menu();
                    continue;
                }
                Doctor doctor = _factory.DoctorManager.CheckPersonalInfo(enteredEmail,enteredPassword);
                if (doctor != null)
                {
                    _factory.DoctorManager.Menu(doctor);
                    continue;
                } 
                Patient patient =_factory.PatientManager.CheckPersonalInfo(enteredEmail, enteredPassword); 
                if (patient != null)
                {
                   _factory.PatientManager.Menu(patient);
                   continue;
                }
                Secretary secretary = _factory.SecretaryManager.CheckPersonalInfo(enteredEmail, enteredPassword);
                if (secretary != null)
                {
                   SecretaryManager.Menu();
                }
                else
                {
                   Console.WriteLine("Wrong username or password! Try again.");
                }
            }
        }
    }
}
