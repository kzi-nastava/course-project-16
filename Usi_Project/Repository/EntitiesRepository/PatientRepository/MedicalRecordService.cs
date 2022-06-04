using System;
using System.Collections.Generic;
using Usi_Project.Users;

namespace Usi_Project.Repository
{
    public class MedicalRecordService
    {
        public static void PatientHistoryMenu(Patient patient)
        {
            bool flag = true;
            while (flag)
            {
                string inp;
                Console.WriteLine("************************");
                Console.WriteLine("*     History Menu     *");
                Console.WriteLine("* [1] Show All History *");
                Console.WriteLine("* [2] Search History   *");
                Console.WriteLine("* [X] For aborting     *");
                Console.WriteLine("************************");
                Console.Write("Your input: ");
                inp = Console.ReadLine();
                if (inp == "X")
                    inp = inp.ToLower();
                switch (inp)
                {
                    case "1":
                        PatientHistory(patient);
                        break;
                    case "2":
                        SearchPatientHistory(patient);
                        break;
                    case "x":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input, try again...");
                        break;
                }

            }
        }
        public static void PatientHistory(Patient patient)
        {
            List<Anamnesa> li = PatientManager._factory.AnamnesaManager.ResolveAnamnesisForEmail(patient.email, 1);
            Console.WriteLine("**************************");
            Console.WriteLine("List of Anamnesis:");
            Console.WriteLine("#############################################");
            foreach (Anamnesa anamnesa in li)
            {
                Console.WriteLine(PatientManager._factory.AnamnesaManager.FormatAnamnesis(anamnesa));
                Console.WriteLine("#############################################");
            }

            string inp;
            Console.Write("Do You Wish To Sort Anamnese[Y/N]: ");
            inp = Console.ReadLine();
            if (inp.ToLower() == "y")
            {
                li.Sort(delegate(Anamnesa x, Anamnesa y) { return x.EmailDoctor.CompareTo(y.EmailDoctor);});
                foreach (Anamnesa anamnesa in li)
                {
                    Console.WriteLine(PatientManager._factory.AnamnesaManager.FormatAnamnesis(anamnesa));
                }
            }
        }

        public static void SearchPatientHistory(Patient patient)
        {
            List<Anamnesa> li = PatientManager._factory.AnamnesaManager.ResolveAnamnesisForEmail(patient.email, 1);
            while (true)
            {
                string inp;
                Console.Write("Enter Keyword: ");
                inp = Console.ReadLine();
                foreach (Anamnesa anamnesa in li)
                {
                    if (anamnesa.Anamnesa1.Contains(inp))
                    {
                        Console.WriteLine(PatientManager._factory.AnamnesaManager.FormatAnamnesis(anamnesa));
                    }
                }
                Console.Write("Do You Wish To Continue Search[Y/N]: ");
                inp = Console.ReadLine();
                if (inp.ToLower() != "y")
                {
                    break;
                }

            }
        }
    }
}