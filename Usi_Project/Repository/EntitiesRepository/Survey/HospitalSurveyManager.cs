using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class HospitalSurveyManager
    {
        private readonly string _hospitalSurveyFN;
        private List<HospitalSurvey> _hospitalS;
        public static Factory _factory;

        public HospitalSurveyManager(string hospitalSurveyFn, Factory factory)
        {
            _hospitalSurveyFN = hospitalSurveyFn;
            _factory = factory;
        }

        public string HospitalSurveyFn => _hospitalSurveyFN;

        public List<HospitalSurvey> HospitalS
        {
            get => _hospitalS;
            set => _hospitalS = value;
        }

        public static Factory Factory
        {
            get => _factory;
            set => _factory = value;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _hospitalS = JsonConvert.DeserializeObject<List<HospitalSurvey>>(File.ReadAllText(_hospitalSurveyFN), json);
        }
    }
}