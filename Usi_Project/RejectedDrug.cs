using System;
using System.Collections.Generic;

namespace Usi_Project
{
    public class RejectedDrug : Drug
    {
        private string _rejection;

        public RejectedDrug(string id, string drugName, string producer, int expirationTime, Verification verification, List<string> ingredients, string rejection) : base(id, drugName, producer, expirationTime, verification, ingredients)
        {
            _rejection = rejection;
        }

        public RejectedDrug(Drug drug, string rejection) : base(drug)
        {
            _rejection = rejection;
        }
        public RejectedDrug()
        {
            _rejection = "";
        }
        
        public RejectedDrug(string rejection)
        {
            _rejection = rejection;
        }

        public string Rejection
        {
            get => _rejection;
            set => _rejection = value;
        }

        public RejectedDrug(string id, string drugName, string producer, int expirationTime, List<string> ingredients, string rejection) : base(id, drugName, producer, expirationTime, ingredients)
        {
            _rejection = rejection;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("REJECTION MESSAGE: " + _rejection);
        }
    }
}