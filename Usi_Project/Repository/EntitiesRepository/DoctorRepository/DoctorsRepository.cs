using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;
using System.Text;
using System.Threading.Channels;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;
using Usi_Project.Repository.EntitiesRepository.DoctorRepository;
using Usi_Project.Settings;


namespace Usi_Project.Repository
{
    public class DoctorsRepository
    {
        
        public List<Doctor> Doctors
        {
            get => _doctors;
            set => _doctors = value;
        }

        private string _doctorFilename;
        private List<Doctor> _doctors;
        private Factory _manager;
        private FileSettings file;

        public DoctorsRepository(string doctorFilename, Factory manager)
        {
            _doctorFilename = doctorFilename;
            _manager = manager;
        }
        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(_doctorFilename), json);

        }



         public void Menu(Doctor doctor)
         {
             DaysOffRequestsConfirmationService dORCS = new DaysOffRequestsConfirmationService();
             dORCS.NotifyTheDoctor(doctor);
             ValidationService validation = new ValidationService(_manager);
             FindService finder = new FindService(_manager);
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the appointment.");
            Console.WriteLine("2) - Schedule overview.");
            Console.WriteLine("3) - Update appointment.");
            Console.WriteLine("4) - Delete appointment");
            Console.WriteLine("5) - Cancel appointment");
            Console.WriteLine("6) - Overview or update medical record");
            Console.WriteLine("7) - Cure verification");
            Console.WriteLine("8) - Day Off request");
            Console.WriteLine("x) - Exit");
            Console.WriteLine("Choose: ");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    ScheduleService sscApp = new ScheduleService(doctor, _manager, validation);
                    sscApp.CreatingPatientAppointment(doctor);
                    break;
                
                case "2":
                    ScheduleService ssc = new ScheduleService(doctor, _manager, validation);
                    ssc.OverviewSchedule(doctor); 
                    break;
                
                case "3":
                    ScheduleService sscUpdate = new ScheduleService(doctor, _manager, validation);
                    sscUpdate.UpdateAppointments(doctor);
                    break;
                
                case "4":
                    ScheduleService sscDel = new ScheduleService(doctor, _manager, validation);
                    sscDel.DeleteAppointment(doctor);
                    break;
                case "5":
                    ScheduleService sscCcl = new ScheduleService(doctor, _manager, validation);

                    DateTime t = ScheduleService.CreateDate();
                    sscCcl.CancelAppointment(t);
                    break;
                case "6":
                    ReferalService referal = new ReferalService(doctor, _manager, validation, finder);
                    RecipeService recipe = new RecipeService(doctor, _manager, validation, finder);
                    OverviewService overviewService = new OverviewService(doctor, _manager, validation, finder, referal, recipe);
                    overviewService.Overview(doctor);
                    break;
                case "7":
                    DrugService drugService = new DrugService(_manager, validation, finder);
                    drugService.DrugsVerification();
                    break;
                    
                case "8":
                    DayOffRequestView dayoffReqView = new DayOffRequestView();
                    DayOffRequest dayOff = dayoffReqView.RequestForDayOff(doctor);
                    List<DayOffRequest> listDaysOff = _manager.DayOffRepository.DaysOff;
                    listDaysOff.Add(dayOff);
                    _manager.Saver.SaveDayOff(listDaysOff);
                    Menu(doctor);

                    break;

                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu(doctor);
                    break;
            }
        }
         
         public Doctor CheckPersonalInfo(string email, string password)
         {
             foreach (Doctor doctor in _manager.DoctorsRepository.Doctors)
             {
                 if (email == doctor.email && password == doctor.password)
                 {
                     return doctor;
                 }
             }
             return null;
         }

        

    }
}
    
            
            






