using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Usi_Project.Users;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Channels;
using Usi_Project.Appointments;
using Usi_Project.Settings;


namespace Usi_Project.Manage
{
    public class DoctorManager
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

        public DoctorManager(string doctorFilename, Factory manager)
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

        public Doctor CheckPersonalInfo(string email, string password)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email && password == doctor.password)
                {
                    return doctor;
                }
            }
            return null;
        }
        
        public bool CheckEmail(string email)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (email == doctor.email)
                {
                    return true;
                }
            }
            return false;
        }
         public void Menu(Doctor doctor)
        {
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the appointment.");
            Console.WriteLine("2) - Schedule overview.");
            Console.WriteLine("3) - Update appointment.");
            Console.WriteLine("4) - Delete appointment");
            Console.WriteLine("5) - Cancel appointment");
            Console.WriteLine("6) - Overview or update medical record");
            Console.WriteLine("x) - Exit");
            Console.WriteLine("Choose: ");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    CreatingPatientAppointment(doctor);
                    break;
                
                case "2":
                    OverviewSchedule(doctor);
                    break;
                
                case "3":
                    UpdateAppointments(doctor);
                    break;
                
                case "4":
                    DeleteAppointment(doctor);
                    break;
                
                case "5":
                    DateTime t = CreateDate();
                    CancelAppointment(t);
                    break;
                
                case "6":
                    Overview(doctor);
                    break;

                case "x":
                    break;
                
                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu(doctor);
                    break;
            }
        }

        public bool checkTime(DateTime dateStart,DateTime dateEnd,Doctor doctor)
        {
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if ((dateStart>appointment.StartTime && dateStart<appointment.EndTime && doctor.email==appointment.EmailDoctor)||
                    (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && doctor.email==appointment.EmailDoctor))

                {
                    
                    return false;
                }
            }

            return true;
        }
        public bool CheckRoom(DateTime dateStart,DateTime dateEnd,string roomId)
        {
            
            foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && roomId==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && roomId==appointment.IdRoom))
                    {
                        return false;
                    }
                }

            return true;
        }
        
        public string CheckRoomOverview(DateTime dateStart,DateTime dateEnd)
        {
            int x = 0;
            foreach (var room in _manager.RoomManager.OverviewRooms)
            {
                foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && room.Id==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && room.Id==appointment.IdRoom))
                    {
                        x = 1;
                    }
                }

                if (x != 1)
                {
                    return room.Id;
                }

            }
            
            return null;
        }
      
        public string CheckOperation(DateTime dateStart,DateTime dateEnd)
        {
            int x = 0;
            foreach (var room in _manager.RoomManager.OperatingRooms)
            {
                foreach (var appointment in _manager.AppointmentManager.Appointment)
                {
                    if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && room.Id==appointment.IdRoom)||
                        (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && room.Id==appointment.IdRoom))
                    {
                        x = 1;
                    }
                }

                if (x != 1)
                {
                    return room.Id;
                }

            }
            
            return null;
        }

        public bool CheckAnnualLeave()
        {
            return true;
        }
            
        public void OverviewSchedule(Doctor doctor)
        {
            DateTime time = CreateDate();
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime >= time &&
                    appointment.EndTime <= time.AddDays(3))
                {
                    Console.WriteLine(appointment.StartTime);
                    appointment.PrintAppointment();

                }
                
            }
        }
      




        public void PrintDoctorsAppointments(Doctor doctor)
        {
            foreach (var app in _manager.AppointmentManager.Appointment)
            {
                if (app.EmailDoctor == doctor.email)
                {
                    app.PrintAppointment();
                }
                
            }
        }

        public void AddReferralForSpecialist(Referral refferal,Patient patient)
        {
            List<Patient> patientList = _manager.PatientManager.Patients;
            foreach (var p in patientList)

            {
                if (p.email == patient.email)
                {
                    p.MedicalRecord.referral = refferal;
                    _manager.Saver.SavePatient(patientList);
                    break;
                }
                
            }
                
            
        }
        

        public void ReferralForSpecialist(Doctor doctor, Patient patient)
        {
            Console.WriteLine("Choose option");
            Console.WriteLine("1) - Referral to a doctor.");
            Console.WriteLine("2) - Referral for a doctor of specialty.");
            var chosenOption = Console.ReadLine();

            switch (chosenOption)
            {
                case "1":
                    Console.WriteLine("Enter email doctor");
                    string DoctorEmail = Console.ReadLine();
                    Referral referralDoctor  = new Referral(patient.email,DoctorEmail,null);
                    AddReferralForSpecialist(referralDoctor,patient);

                    break;

                case "2":
                    Console.WriteLine("Enter the specialty doctor");
                    var specialisation = Console.ReadLine();
                    Referral referralSpecialisation  = new Referral(patient.email,null, specialisation);
                    AddReferralForSpecialist(referralSpecialisation,patient);
                    

                    break;
            }
        }

        public void CreateRecipes(string emailPatient)
        {
            List<Recipes> recipes = _manager.RecipesManager.Recipes;
            Console.WriteLine("Enter cure name: ");
            string cureName = "Cure name: " + Console.ReadLine();
            Console.WriteLine("When to take medicine? ");
            string timeInstructions = "When to take medicine: " + Console.ReadLine();
            Console.WriteLine("How many times a day should he take medicine? ");
            string timesADay = "How many times a day should he take medicine: " + Console.ReadLine();
            Console.WriteLine("Choose option");
            Console.WriteLine("1) - Drink before meal.");
            Console.WriteLine("2) - Drink after meal.");
            Console.WriteLine("3) - Doesnt matter.");
            var chosenOption = Console.ReadLine();

            switch (chosenOption)
            {
                case "1":
                    string timeRelFood = "Drink before meal.";
                    Recipes recipe = new Recipes(cureName,emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _manager.Saver.SaveRecipe(recipes);
                    break;
                    
                    

                case "2":
                    timeRelFood = "Drink after meal.";
                    recipe = new Recipes(cureName,emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _manager.Saver.SaveRecipe(recipes);
                    break;
                case "3":
                    timeRelFood = "Whenever, it is not related to food.";
                    recipe = new Recipes(cureName,emailPatient, timeInstructions, timesADay, timeRelFood);
                    recipes.Add(recipe);
                    _manager.Saver.SaveRecipe(recipes);
                    break;

                    
            }
            

        }
        

        public void UpdateAppointments(Doctor doctor)
        {

            Console.WriteLine("Enter start time of appointment");
            var time = DateTime.Parse(Console.ReadLine());
            List<Appointment> appList = _manager.AppointmentManager.Appointment;
            foreach (var app in appList)

            {
                if (time == app.StartTime)
                {
                    while (true)
                    {
                        Console.WriteLine("1) - Update time.");
                        Console.WriteLine("2) - Update room.");
                        Console.WriteLine("3) - Update all.");
                        Console.WriteLine("x) - exit");
                        var chosenOption = Console.ReadLine();

                        switch (chosenOption)
                        {
                            case "1":
                                Console.WriteLine("Enter start time");
                                DateTime stime = CreateDate();
                                Console.WriteLine("Enter end time");
                                DateTime etime = CreateDate();
                                app.StartTime = stime;
                                app.EndTime = etime;
                                if (checkTime(stime, etime, doctor))
                                {
                                    _manager.Saver.SaveAppointment(appList);

                                }

                                break;

                            case "2":
                                Console.WriteLine("Enter room id");
                                var idroom = Console.ReadLine();
                                if (CheckRoom(app.StartTime, app.EndTime, idroom))
                                {
                                    app.IdRoom = idroom;
                                    _manager.Saver.SaveAppointment(appList);
                                }

                                break;

                            case "3":
                                Console.WriteLine("Enter start time");
                                DateTime stimes = CreateDate();
                                Console.WriteLine("Enter end time");
                                DateTime etimes = CreateDate();
                                app.StartTime = stimes;
                                app.EndTime = etimes;
                                if (checkTime(stimes, etimes, doctor))
                                {
                                    var idrooms = Console.ReadLine();
                                    if (CheckRoom(app.StartTime, app.EndTime, idrooms))
                                    {
                                        app.IdRoom = idrooms;
                                        app.StartTime = stimes;
                                        app.StartTime = etimes;
                                        _manager.Saver.SaveAppointment(appList);

                                    }
                                }

                                break;

                            case "x":
                                break;

                        }
                    }
                }
            }
        }



        public void DeleteAppointment(Doctor doctor)
        {
            PrintDoctorsAppointments(doctor);
            Console.WriteLine("Enter start time of appointment");
            DateTime time = CreateDate(); 
            List<Appointment> appointments = _manager.AppointmentManager.Appointment;
            foreach (var appointment in appointments)
            {
                if (appointment.StartTime == time)
                {
                    appointments.Remove(appointment);
                    break;
                }
                
            }
            
            _manager.Saver.SaveAppointment(appointments);

        }

        public DateTime CreateDate()
        {
            Console.WriteLine("Enter Month");
            int monthStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Day");
            int dayStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Hour");
            int hourStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Minute");
            int minuteStart = Convert.ToInt32(Console.ReadLine());
            var time = new DateTime(DateTime.Now.Year, monthStart, dayStart, hourStart,
                minuteStart, 0);
            return time;
        }
        
        public DateTime TodayAppointment()
        {
            Console.WriteLine("Enter Hour");
            int hourStart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Minute");
            int minuteStart = Convert.ToInt32(Console.ReadLine());
            var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hourStart,
                minuteStart, 0);
            return time;
            
        }



        public void CreatingPatientAppointment(Doctor doctor)
        {
            string patientEmail;
            while (true)
            {
                Console.WriteLine("Enter the Email of the patient:");
                patientEmail = Console.ReadLine();
                if (!_manager.PatientManager.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail doesn't exists, try again.");
                }
                else
                {
                    break;   
                }
                    
            }
            

            Console.WriteLine("Enter type ('OP' or 'OV')");
            string type = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("1) - Review of own overreviews");
                Console.WriteLine("2) - Make an appointment");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    OverviewSchedule(doctor);
                }
                else
                {
                while (true)
                {
                    DateTime startTime = CreateDate();
                    if (type == "OV")
                    {
                        DateTime endTime = startTime.AddMinutes(15);
                        var idRoom = CheckRoomOverview(startTime, endTime);
                        if (checkTime(startTime, endTime, doctor))
                        {
                            if (idRoom == null)
                            {
                                Console.WriteLine("All rooms are busy in this term");
                                break;
                                
                            }
                                
                            List<Appointment> appointments = _manager.AppointmentManager.Appointment;
                            Appointment app= new Appointment(doctor.email,
                                patientEmail,
                                startTime, endTime, type, idRoom,"0");
                            appointments.Add(app);
                            _manager.Saver.SaveAppointment(appointments);
                            break;
                            
                            

                            
                                
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter duration of operation in minutes");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        DateTime endTime = startTime.AddMinutes(duration);

                        if (checkTime(startTime, endTime, doctor))
                        {
                            var idRoom = CheckRoomOverview(startTime, endTime);

                            if (idRoom != null)
                            {
                                List<Appointment> appointments = _manager.AppointmentManager.Appointment;
                                Appointment appointment= new Appointment(doctor.email,
                                    patientEmail,
                                    startTime, endTime, type, idRoom,"0");
                                _manager.AppointmentManager.Appointment.Add(appointment);
                               
                                _manager.Saver.SaveAppointment(appointments);
                                break;
                            }

                        }

                        Console.WriteLine("You fell into some term ");
                        OverviewSchedule(doctor);

                    }
                }

                }

                break;
            }


        }

        public Appointment FindAppointment(DateTime time, Doctor doctor)
        {
            foreach (var appointment in _manager.AppointmentManager.Appointment)
            {
                if (appointment.EmailDoctor == doctor.email && appointment.StartTime == time)
                {
                    return appointment;
                }
                
            }

            return null;
        }




        public string GenerateRandomString()
        {

            int length = 7;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            System.Console.WriteLine(str_build.ToString());
            return str_build.ToString();
        }

        public void WriteAnamnesa(Appointment appointment)
        {
            Console.WriteLine("Napisite anamnesu");

            string anamnesaString = Console.ReadLine();
            string id = GenerateRandomString();
            Anamnesa anamnesa = new Anamnesa(id, appointment.EmailDoctor, appointment.EmailPatient, anamnesaString);
            List<Anamnesa> listAnamnesa = _manager.AnamnesaManager.Anamnesa;
            listAnamnesa.Add(anamnesa);
            _manager.Saver.SaveAnamnesa(listAnamnesa);
        }
        

        public void UpdateMedicalRecord(string email)
        {
            List<Patient> patients = _manager.PatientManager.Patients;
            foreach (var patient in patients)
            {
                if (patient.email == email)
                {
                    Console.WriteLine("Enter what u want to change or press enter to skip");
                    Console.WriteLine("Enter height");
                    var changeHeight = Console.ReadLine();
                    if (changeHeight != "")
                    {
                        patient.MedicalRecord.height = changeHeight;
                    }
            
                    Console.WriteLine("Enter weight");
                    var changeWeight = Console.ReadLine();
                    if (changeWeight != "")
                    {
                        patient.MedicalRecord.weight = changeWeight;
                    }
                    Console.WriteLine("Enter diseases");
                    var changeDiseases = Console.ReadLine();
                    if (changeDiseases != "")
                    {
                        patient.MedicalRecord.diseases = changeWeight;
                    }
                    Console.WriteLine("Enter Alergens");
                    var changeAlergens = Console.ReadLine();
                    if (changeAlergens != "")
                    {
                        patient.MedicalRecord.allergens = changeAlergens;
                    }
                    _manager.Saver.SavePatient(patients);
                    
                }
                
            }
            
            
        }

        public void UpdateStatus(DateTime time)
        {
            List<Appointment> list = _manager.AppointmentManager.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "1";
                    _manager.Saver.SaveAppointment(list);
                    break;
                }
            }
        }
        public void CancelAppointment(DateTime time)
        {
            List<Appointment> list = _manager.AppointmentManager.Appointment;
            foreach (var app in list)
            {
                if (app.StartTime == time)
                {
                    app.Status = "2";
                    _manager.Saver.SaveAppointment(list);
                    break;
                }
            }
        }

        public void Overview(Doctor doctor)
        {
            DateTime time= TodayAppointment();
            Appointment app=FindAppointment(time, doctor);
            while (true)
            {
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Update medicalRecord.");
                Console.WriteLine("2) - Write anamnesa.");
                Console.WriteLine("x) - exit.");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        UpdateMedicalRecord(app.EmailPatient);
                        break;

                    case "2":
                        WriteAnamnesa(app);
                        Console.WriteLine("Choose one of the options below: ");
                        Console.WriteLine("1) - Issuing a prescription.");
                        Console.WriteLine("2) - End overview.");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                          case "1":
                              CreateRecipes(app.EmailPatient);
                              UpdateStatus(app.StartTime);
                              break;
                          case "2":
                              UpdateStatus(app.StartTime);
                              break;
                        }

                        break;
                }
                Console.WriteLine("Choose one of the options below: ");
                Console.WriteLine("1) - Recipes for patient.");
                Console.WriteLine("x) - exit.");
                var option1 = Console.ReadLine();
                switch (option1)
                {
                    case "1":
                        CreateRecipes(app.EmailPatient);
                        break;
                    case "x":
                        
                        break;
                }
                
                UpdateRoomEquipment(app);
                

            }
        }

       

        void UpdateRoomEquipment(Appointment app)
        {
            foreach (var room in _manager.RoomManager.OverviewRooms)
            {
                if (room.Id == app.IdRoom)
                {
                    while (true)
                    {
                        Dictionary<int, DynamicEquipment> dictionary = new Dictionary<int, DynamicEquipment>();
                        int i = 1;
                        foreach (DynamicEquipment equipment in
                                 (DynamicEquipment[]) Enum.GetValues(typeof(DynamicEquipment)))
                        {
                            Console.WriteLine(i + ") " + equipment);
                            dictionary[i] = equipment;
                            i++;
                        }

                        _manager.SecretaryManager.PrintEquipmentForOverviewRoom(app.IdRoom);
                        Console.Write("Enter option or 0 for exit>> ");
                        int option = Int32.Parse(Console.ReadLine());
                        if (option == 0)
                        {
                            _manager.RoomManager.SaveData();
                            return;
                        }
                            DynamicEquipment chosen = dictionary[option];
                        while (true)
                        {
                            Console.WriteLine("Enter num");
                            var num = Convert.ToInt32(Console.ReadLine());
                            if (room.DynamicEquipment[chosen] - num > 0)
                            {
                                room.DynamicEquipment[chosen] -= num;
                                break;


                            }
                        }
                    }



                }
            }
        }
        // public void CureNotVerified()
        // {
        //     List<Drug> drugs = _manager.DrugManager.Drugs;
        //     foreach(Drug drug  in drugs)
        //     {
        //         if (drug.Verification == Verification.NOT_VERIFIED)
        //             drug.Print();
        //     }
        // }
        //
        //
        // public Drug VerifyDrug(Drug drug)
        // {
        //     Console.WriteLine("Verify Drug");
        //     Console.WriteLine("1) Drug Accepted" + "\n" + "2) Drug Cancceled");
        //     drug.Print();
        //     var chosenOption = Console.ReadLine();
        //     switch (chosenOption)
        //     {
        //         case "1":
        //             drug.Verification = Verification.VERIFIED;
        //             _manager.DrugManager.SaveData();
        //             return drug;
        //             break;
        //
        //         case "2":
        //             Console.WriteLine("Problem about drug: ");
        //             var problem = Console.ReadLine();
        //             RejectedDrug rejectedDrug = new RejectedDrug(drug, problem);
        //             _manager.DrugManager.RejectedDrugs.Add(rejectedDrug);
        //             _manager.DrugManager.SaveData();
        //             return rejectedDrug;
        //
        //             
        //     }
        //
        //     return null;
        // }
        //
        // public void CureVerification()
        // {
        //     List<Drug> drugs = _manager.DrugManager.Drugs;
        //     while(true)
        //     {
        //         CureNotVerified();
        //         Console.WriteLine("Unesite id droge koju zelite");
        //         Console.WriteLine("Unesite x za kraj");
        //         string id = Console.ReadLine();
        //         foreach(Drug d in drugs)
        //         {
        //             if (d.Id == id)
        //                 VerifyDrug(d);
        //         }
        //
        //         if (id == "x")
        //             return;
        //
        //     }
            
            

       // }
        
           
              

    }
}
    
            
            






