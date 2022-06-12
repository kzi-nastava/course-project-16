using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Specialized;
using Usi_Project.Users;
using Usi_Project.Appointments;
using Usi_Project.DoctorFuncions;

namespace Usi_Project.Repository
{
    public class SecretaryManager
    {
        private string _secretaryFilename;
        private List<Secretary> _secretaries;
        public static Factory _manager;

        public SecretaryManager(string secretaryFilename, Factory factory)
        {
            _secretaryFilename = secretaryFilename;
            _manager = factory;
        }

        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            _secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(_secretaryFilename), json);

        }

        public Secretary CheckPersonalInfo(string email, string password)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email && password == secretary.password)
                {
                    return secretary;
                }

            }

            return null;
        }

        public bool CheckEmail(string email)
        {
            foreach (Secretary secretary in _secretaries)
            {
                if (email == secretary.email)
                {
                    return true;
                }
            }

            return false;
        }

        public static void Menu()
        {
            Refresh();
            Console.WriteLine("Choose one of the options below: ");
            Console.WriteLine("1) - Create the patient profile.");
            Console.WriteLine("2) - Change the patient profile.");
            Console.WriteLine("3) - Delete the patient profile.");
            Console.WriteLine("4) - Block the patient profile.");
            Console.WriteLine("5) - Unblock the patient profile.");
            Console.WriteLine("6) - Confirmation of a request.");
            Console.WriteLine("7) - Schedule referal appointment");
            Console.WriteLine("8) - Emergency appointment.");
            Console.WriteLine("9) - Request dynamic equipment.");
            Console.WriteLine("10) - Deployment of dynamic equipment.");
            Console.WriteLine("x) - Exit.");
            string chosenOption = Console.ReadLine();
            switch (chosenOption)
            {
                case "1":
                    PatientCrudService pCS = new PatientCrudService();
                    pCS.CreatingPatientProfile();
                    break;

                case "2":
                    PatientCrudService pCS2 = new PatientCrudService();
                    pCS2.ChangingPatientProfile();
                    break;

                case "3":
                    PatientCrudService pCS3 = new PatientCrudService();
                    pCS3.DeletingPatientProfile();
                    break;

                case "4":
                    PatientBlockService pBS = new PatientBlockService();
                    pBS.BlockingPatientProfile();
                    break;

                case "5":
                    PatientBlockService pBS2 = new PatientBlockService();
                    pBS2.UnblockingPatientProfile();
                    break;
                case "6":
                    PatientRequestService pRS = new PatientRequestService();
                    pRS.ConfirmationOfRequests();
                    break;
                case "7":
                    ReferalConfirmationService rCS = new ReferalConfirmationService();
                    rCS.ReferalAppointment();
                    break;
                case "8":
                    EmergencyAppointmentService eAS = new EmergencyAppointmentService();
                    eAS.EmergencyAppointment();
                    break;
                case "9":
                    DynamicEquipmentRequestService dERS = new DynamicEquipmentRequestService();
                    dERS.DynamicEquipmentRequest();
                    break;
                case "10":
                    DynamicEquipmentDeploymentService dEDS = new DynamicEquipmentDeploymentService();
                    dEDS.DeploymentOfDynamicEquipment();
                    break;
                case "x":
                    break;

                default:
                    Console.WriteLine("Invalid option entered, try again");
                    Menu();
                    break;
            }
        }
        

       
        

        static void Refresh()
        {
            DateTime currentTime = DateTime.Now;
            for (int i = 0; i < _manager.DynamicRequestManager.DynamicRequests.Count; i++)
            {
                if (currentTime > _manager.DynamicRequestManager.DynamicRequests[i].TimeOfAddition)
                {
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Gauze] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Gauze];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Buckles] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Buckles];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Bandages] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Bandages];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Paper] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Paper];
                    _manager.RoomManager.StockRoom.DynamicEquipment[DynamicEquipment.Pencils] +=
                        _manager.DynamicRequestManager.DynamicRequests[i].DynamicEquipment[DynamicEquipment.Pencils];
                    _manager.DynamicRequestManager.DynamicRequests.Remove(_manager.DynamicRequestManager.DynamicRequests[i]);
                }
            }

            using (StreamWriter file = File.CreateText(_manager.DynamicRequestManager.RequestFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _manager.DynamicRequestManager.DynamicRequests);
            }
            _manager.RoomManager.SaveData();
        }
        
        
    }
}