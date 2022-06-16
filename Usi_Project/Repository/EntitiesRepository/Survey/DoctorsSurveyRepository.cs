using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.Survey
{
    public class DoctorsSurveyRepository
    {
        private readonly string _doctorSurveyFN;
        private List<DoctorSurvey> _docotrS;
        public static Factory _factory;

        public DoctorsSurveyRepository(string doctorSurveyFn, Factory factory)
        {
            _doctorSurveyFN = doctorSurveyFn;
            _factory = factory;
        }

        public string DoctorSurveyFn => _doctorSurveyFN;

        public List<DoctorSurvey> DocotrS
        {
            get => _docotrS;
            set => _docotrS = value;
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
            _docotrS = JsonConvert.DeserializeObject<List<DoctorSurvey>>(File.ReadAllText(_doctorSurveyFN), json);
        }
        
        
        
    }
}