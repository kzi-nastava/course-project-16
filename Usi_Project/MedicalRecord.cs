namespace Usi_Project
{
    public class MedicalRecord
    {
        
        public string height;
        public string weight;
        public string diseases;
        public string allergens;
        public Referral referral;

        public MedicalRecord()
        {
        }

        public MedicalRecord(string height, string weight, string diseases, string allergens, Referral referral)
        {
            this.height = height;
            this.weight = weight;
            this.diseases = diseases;
            this.allergens = allergens;
            this.referral = referral;
        }
       

        public MedicalRecord( string height, string weight, string diseases, string allergens)
        {
            this.height = height;
            this.weight = weight;
            this.diseases = diseases;
            this.allergens = allergens;
            this.referral = null;
        }
    }
}