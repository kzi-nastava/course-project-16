

using System;
using Usi_Project.Appointments;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public static class AntiTrollService
    {
        public static bool CheckBlockedStatus(Patient patient)
        {
            if (patient.Blocked == 2)
            {
                Console.WriteLine("Sorry, Your Account Has Been Blocked" +
                                  "Contact Support, Much Luck!");
                return false;
            }

            return true;
        }

        public static bool AntiTroll(Patient patient)
        {
            /*bool flag = CheckBlockedStatus(patient);
            
            if (!flag)
                return false;
            
            int changed = 0, deleted = 0, created = 0;
             // Counting Changes, Deletion, Appointments
            foreach (Requested req in PatientsRepository._factory.RequestManager.Requested)
            {
                 if (req.EmailPatient == patient.email)
                 {
                     if (req.Operation == "1")
                     {
                         changed++;
                     } 
                     else if (req.Operation == "2")
                     {
                         deleted++;
                     } 
                     else if (req.Operation == "3"){ 
                         created++;
                     }
                 }
            }
            if(changed > 4){
                 Console.WriteLine("In The Past Month You Have Changed Appointments More Then 4 Times!");
                 flag = false;
            }else if(created > 8) 
            {
                 Console.WriteLine("In The Past Month You Have Generated More Than 8 Requests!");
                 flag = false;
            }else if(deleted >4)
            {
                 Console.WriteLine("In The Past Month You Have Canceled More Than 4 Appointments!");
                 flag = false;
            }
            if (!flag)
            {
                 Console.WriteLine("Sorry, Your Account Has Been Blocked" +
                                   "Contact Support, Much Luck!");
                 return false;
            }*/

            return true;
        }
    }
}
