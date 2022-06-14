using System;

namespace Usi_Project
{
    public class Referral
    {
        public string PatientEmail;
        public string DoctorEmail;
        public string DoctorSpecialisation;

        public Referral(string patientEmail, string doctorEmail, string doctorSpecialisation)
        {
            PatientEmail = patientEmail;
            DoctorEmail = doctorEmail;
            DoctorSpecialisation = doctorSpecialisation;
        }
        

        public string PatientEmail1
        {
            get => PatientEmail;
            set => PatientEmail = value;
        }

        public string DoctorEmail1
        {
            get => DoctorEmail;
            set => DoctorEmail = value;
        }

        public string DoctorSpecialisation1
        {
            get => DoctorSpecialisation;
            set => DoctorSpecialisation = value;
        }

        public void toString()
        {
            Console.WriteLine(@"Patient email: " + PatientEmail +
                              "Doctor email: " + DoctorEmail +
                              "Doctor specialisation: " + DoctorSpecialisation);
        }
    }
}