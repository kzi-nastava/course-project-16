
namespace Usi_Project

{
    public class Recipes
    {
        public string cureName;
        public string emailPatient;
        public string timeInstructions;
        public string timesADay;
        public string timeRelFood;

        public string CureName
        {
            get => cureName;
            set => cureName = value;
        }

        public string EmailPatient
        {
            get => emailPatient;
            set => emailPatient = value;
        }

        public string TimeInstructions
        {
            get => timeInstructions;
            set => timeInstructions = value;
        }

        public string TimesADay
        {
            get => timesADay;
            set => timesADay = value;
        }

        public string TimeRelFood
        {
            get => timeRelFood;
            set => timeRelFood = value;
        }

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