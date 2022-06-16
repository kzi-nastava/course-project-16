using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Usi_Project.Manage;
using Usi_Project.Repository.EntitiesRepository.Survey;
using Usi_Project.Users;

namespace Usi_Project.Repository.EntitiesRepository.DirectorsRepository
{
    public class DirectorRepository
    {
        private readonly string _directorFilename;
        private Director _director;
        private static RoomRepository _roomRepository;
        private static HospitalSurveyManager _hospitalSurvey;
        private static DoctorsSurveyRepository _doctorSurvey;
        private static TimerManager _timerManager;
        private static DrugsRepository _drugsRepository;
        private static DoctorsRepository _doctorsRepository;
        private static RoomService _roomService;

        public DirectorRepository(string directorFilename, RoomRepository roomRepository,
                                HospitalSurveyManager hospitalSurvey, DoctorsSurveyRepository doctorSurvey,
                                TimerManager timerManager, DrugsRepository DrugsRepository, DoctorsRepository DoctorsRepository)
                                
        {
            _directorFilename = directorFilename;
            _roomRepository = roomRepository;
            _hospitalSurvey = hospitalSurvey;
            _doctorSurvey = doctorSurvey;
            _doctorsRepository = DoctorsRepository;
            _timerManager = timerManager;
            _drugsRepository = DrugsRepository;
            _roomService = new RoomService(roomRepository);
        }
        
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _director = JsonConvert.DeserializeObject<Director>(File.ReadAllText(_directorFilename), json);
        }

        public string DirectorFilename => _directorFilename;

        public Director Director
        {
            get => _director;
            set => _director = value;
        }

        public  RoomRepository RoomRepository
        {
            get => _roomRepository;
            set => _roomRepository = value;
        }
        
        public RoomService RoomService
        {
            get => _roomService;
        }


        public  HospitalSurveyManager HospitalSurvey
        {
            get => _hospitalSurvey;
            set => _hospitalSurvey = value;
        }

        public  DoctorsSurveyRepository DoctorSurvey
        {
            get => _doctorSurvey;
            set => _doctorSurvey = value;
        }

        public  TimerManager TimerManager
        {
            get => _timerManager;
            set => _timerManager = value;
        }

        public  DrugsRepository DrugsRepository
        {
            get => _drugsRepository;
            set => _drugsRepository = value;
        }

        public  DoctorsRepository DoctorsRepository
        {
            get => _doctorsRepository;
            set => _doctorsRepository = value;
        }

        public void SaveData()
        {
            using (StreamWriter file = File.CreateText(_directorFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _director);
            }
        }
        
        
        public Director CheckPersonalInfo(string email, string password) =>
            email == _director.email && password == _director.password ? _director : null;

        public bool CheckEmail(string email)
        {
            return email == _director.email;
        }
    }
}