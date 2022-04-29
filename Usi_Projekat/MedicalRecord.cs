namespace Usi_Projekat
{
    public class MedicalRecord
    {
        
        public string height;
        public string weight;
        public string diseases;
        public string allergens;

        public MedicalRecord(string height, string weight, string diseases, string allergens)
        {
            this.height = height;
            this.weight = weight;
            this.diseases = diseases;
            this.allergens = allergens;
        }
    }
}