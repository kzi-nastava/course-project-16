using System;
using System.Collections.Generic;
using Usi_Project.Manage;

namespace Usi_Project.Repository.DrugRepository
{
    public class DrugService
    {
        private DrugsRepository _drugsRepository;
        private DrugViewer _drugViewer;

        public DrugService(DrugsRepository drugsRepository)
        {
            _drugsRepository = drugsRepository;
            _drugViewer = new DrugViewer(_drugsRepository, this);
        }
        
        
        public void GetOptions()
        {
            while (true)
            {
                var option = DrugViewer.PrintMenu();
                switch (option)
                {
                    case "1":
                        AddNewDrug();
                        break;
                    case "2":
                        _drugViewer.ViewDrugs();
                        break;
                    case "3":
                        _drugViewer.ViewRejectedDrugs();
                        break;
                    case "x":
                        return;
                }
            }
        }

        private void AddNewDrug()
        {
            IOInfoDrugs.AddNewDrug(_drugsRepository);
        }

        public Drug GetDrug()
        {
            while (true)
            {
                Dictionary<int, Drug> drugs = new Dictionary<int, Drug>();
                int i = 1;
                foreach (var drug in _drugsRepository.Drugs)
                {
                    Console.WriteLine(i + ") " + drug.Name);
                    drugs[i] = drug;
                    i++;
                }
     
                int option = IOInfoDrugs.GetNumberFromCl();
                if (!drugs.ContainsKey(option))
                    continue;
                return drugs[option];
            }
        }
        
        public RejectedDrug GetRejectedDrugs()
        {
            if (_drugsRepository.RejectedDrugs.Count == 0)
             return null;
            
            Dictionary<int, RejectedDrug> drugs = new Dictionary<int, RejectedDrug>();
            int i = 1;
            foreach (var drug in _drugsRepository.RejectedDrugs)
            {
                Console.WriteLine(i + ") " + drug.Name);
                drugs[i] = drug;
                i++;
            }
            var option = IOInfoDrugs.GetNumberFromCl();
            return drugs[option];
        }

    }
}