using System;
using System.Collections.Generic;
using Usi_Project.Manage;
using Usi_Project.Repository;

namespace Usi_Project.DoctorFuncions
{
    public class DrugService
    {
        public Factory _drugManager;
        public ValidationService _validation;
        public FindService _finder;

        public DrugService(Factory drugManager, ValidationService validation, FindService finder)
        {
            _drugManager = drugManager;
            _validation = validation;
            _finder = finder;
        }

        public void PrintDrugsForVerification()
        {
            List<Drug> drugs = _drugManager.DrugsRepository.Drugs;
            foreach(Drug drug  in drugs)
            {
                if (drug.Verification == Verification.NOT_VERIFIED)
                    drug.Print();
            }
        }
        public Drug VerifyDrug(Drug drug)
        {
            Console.WriteLine("Verify Drug");
            Console.WriteLine("1) Drug Accepted" + "\n" + "2) Drug Cancceled");
            drug.Print();
            var chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    drug.Verification = Verification.VERIFIED;
                    _drugManager.DrugsRepository.SaveData();
                    return drug;
                    break;

                case "2":
                    Console.WriteLine("Problem about drug: ");
                    var problem = Console.ReadLine();
                    RejectedDrug rejectedDrug = new RejectedDrug(drug, problem);
                    _drugManager.DrugsRepository.RejectedDrugs.Add(rejectedDrug);
                    _drugManager.DrugsRepository.SaveData();
                    return rejectedDrug;
            }
            return null;
        }
        public void DrugsVerification()
        {
            List<Drug> drugs = _drugManager.DrugsRepository.Drugs;
            while(true)
            {
                PrintDrugsForVerification();
                Console.WriteLine(@"Enter drug id for verification
                                    Enter x for exit");
                string id = Console.ReadLine();
                foreach(Drug d in drugs)
                {
                    if (d.Id == id)
                        VerifyDrug(d);
                }
                if (id == "x")
                    return;
            }
        }
    }
}