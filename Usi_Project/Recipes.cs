namespace Usi_Project
{
    public class Recipes
    {
        public string cureName;
        public string emailPatient;
        public string timeInstructions;
        public string timesADay;
        public string timeRelFood;

        public Recipes(string cureName,string emailPatient, string timeInstructions, string timesADay, string timeRelFood)
        {
            this.cureName = cureName;
            this.emailPatient = emailPatient;
            this.timeInstructions = timeInstructions;
            this.timesADay = timesADay;
            this.timeRelFood = timeRelFood;
        }
    }
    
    
}