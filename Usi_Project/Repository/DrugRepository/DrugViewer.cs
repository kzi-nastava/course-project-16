using System;
using Usi_Project.Manage;
using Usi_Project.Repository.DrugRepository;

namespace Usi_Project.Repository
{
    public class DrugViewer
    {
        private DrugsRepository _drugsRepository;
        private DrugService _drugService;
        public DrugViewer(DrugsRepository drugsRepository, DrugService drugService)
        {
            _drugsRepository = drugsRepository;
            _drugService = drugService;
        }
        public static string PrintMenu()
        {
            while (true)
            { 
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Adding new drugs to the system");
                Console.WriteLine("2) - View drugs");
                Console.WriteLine("3) - View rejected drugs");
                Console.WriteLine("x) - Back");
                Console.Write(">> ");
                string option = Console.ReadLine();
                option ??= "x";
                Console.WriteLine();
                return option;
            }
        }
        public  void ViewDrugs()
        {
            Drug chosen = _drugService.GetDrug();
            chosen.Print();
            Console.Write("Do you want to change some ingredients ? y/n  >> ");
            string answer = Console.ReadLine();
            if (answer == "y")
                chosen.ChangeIngredients();
        }

        public void ViewRejectedDrugs()
        {
            Console.WriteLine("Rejected drugs:");
            RejectedDrug rejectedDrug = _drugService.GetRejectedDrugs();
            if (rejectedDrug == null)
                return;
            
            rejectedDrug.Print();
            Console.Write("Do you want to change some ingredients ? y/n  >> ");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                rejectedDrug.ChangeIngredients();
                _drugsRepository.AddDrug(new Drug(rejectedDrug));
                _drugsRepository.RemoveRejectedDrug(rejectedDrug);
                _drugsRepository.SaveData();
                
            }
        }

    }
}