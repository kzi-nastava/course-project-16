using System;
using System.Collections.Generic;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class ReferalService
    {
        public Doctor doctor;
        public Factory _referalManager;
        public ValidationService _validation;
        public FindService _finder;

        public ReferalService(Doctor doctor, Factory referalManager, ValidationService validation, FindService finder)
        {
            this.doctor = doctor;
            _referalManager = referalManager;
            _validation = validation;
            _finder = finder;
        } 
        
        public void AddReferralForSpecialist(Referral refferal,Patient patient)
        {
            List<Patient> patientList = _referalManager.PatientsRepository.Patients;
            foreach (var p in patientList)

            {
                if (p.email == patient.email)
                {
                    p.MedicalRecord.referral = refferal;
                    _referalManager.Saver.SavePatient(patientList);
                    break;
                }
                
            }
                
            
        } 
        public void ReferralForSpecialist(Doctor doctor, Patient patient)
        {
            Console.WriteLine("Choose option");
            Console.WriteLine("1) - Referral to a doctor.");
            Console.WriteLine("2) - Referral for a doctor of specialty.");
            var chosenOption = Console.ReadLine();

            switch (chosenOption)
            {
                case "1":
                    Console.WriteLine("Enter email doctor");
                    string DoctorEmail = Console.ReadLine();
                    Referral referralDoctor  = new Referral(patient.email,DoctorEmail,null);
                    AddReferralForSpecialist(referralDoctor,patient);

                    break;

                case "2":
                    Console.WriteLine("Enter the specialty doctor");
                    var specialisation = Console.ReadLine();
                    Referral referralSpecialisation  = new Referral(patient.email,null, specialisation);
                    AddReferralForSpecialist(referralSpecialisation,patient);
                    break;
            }
        }
    }
    
}