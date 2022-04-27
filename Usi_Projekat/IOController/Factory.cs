using System.Collections.Generic;
using System.IO;
using Usi_Projekat.Users;

namespace Usi_Projekat.IOController
{
    public class Factory
    {
        public Director Director
        {
            get => director;
            set => director = value;
        }

        public List<Patient> patients;
        public List<Doctor> doctors;
        public List<Secretary> secretaries;
        public Director director;

        public Factory()
        {
            this.patients = new List<Patient>();
            this.doctors = new List<Doctor>();
            this.secretaries = new List<Secretary>();
            this.director = new Director();
        }

        public Factory(List<Patient> patients, List<Doctor> doctors, List<Secretary> secretaries)
        {
            this.patients = patients;
            this.doctors = doctors;
            this.secretaries = secretaries;
        }

        public List<Patient> Patients
        {
            get => patients;
            set => patients = value;
        }

        public List<Doctor> Doctors
        {
            get => doctors;
            set => doctors = value;
        }

        public List<Secretary> Secretaries
        {
            get => secretaries;
            set => secretaries = value;
        }
    }
}