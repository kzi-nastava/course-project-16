namespace Usi_Project
{
    public class Anamnesa
    {
        private string id;
        private string emailDoctor;
        private string emailPatient;
        private string anamnesa;

        public Anamnesa(string id, string emailDoctor, string emailPatient, string anamnesa)
        {
            this.id = id;
            this.emailDoctor = emailDoctor;
            this.emailPatient = emailPatient;
            this.anamnesa = anamnesa;
        }

        public Anamnesa()
        {
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string EmailDoctor
        {
            get => emailDoctor;
            set => emailDoctor = value;
        }

        public string EmailPatient
        {
            get => emailPatient;
            set => emailPatient = value;
        }

        public string Anamnesa1
        {
            get => anamnesa;
            set => anamnesa = value;
        }
    }
}