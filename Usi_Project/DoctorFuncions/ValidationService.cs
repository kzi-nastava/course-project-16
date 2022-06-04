using System;
using Usi_Project.Repository;
using Usi_Project.Users;

namespace Usi_Project.DoctorFuncions
{
    public class ValidationService
    {
        public Factory _validationManager;

        
        public ValidationService(Factory validationManager)
        {
            _validationManager = validationManager;
        }
        public bool CheckTime(DateTime dateStart,DateTime dateEnd,Doctor doctor)
        {
            foreach (var appointment in _validationManager.AppointmentManager.Appointment)
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
            
            foreach (var appointment in _validationManager.AppointmentManager.Appointment)
            {
                if ((dateStart>appointment.StartTime && dateEnd<appointment.EndTime && roomId==appointment.IdRoom)||
                    (dateStart<appointment.StartTime && dateEnd>appointment.StartTime && roomId==appointment.IdRoom))
                {
                    return false;
                }
            }

            return true;
        } 
        public string GetIfFreeOverviewRoom(DateTime dateStart,DateTime dateEnd)
        {
            bool x = true;
            foreach (var room in _validationManager.RoomManager.OverviewRooms)
            {
                foreach (var appointment in _validationManager.AppointmentManager.Appointment)
                {
                    x = CheckRoom(dateStart, dateEnd, room.Id);
                }

                if (x == true)
                {
                    return room.Id;
                }

            }
            return null;
        } 
        public string GetIfFreeOperatingRoom(DateTime dateStart,DateTime dateEnd)
        {
            bool x = true;
            foreach (var room in _validationManager.RoomManager.OperatingRooms)
            {
                foreach (var appointment in _validationManager.AppointmentManager.Appointment)
                {
                    x = CheckRoom(dateStart, dateEnd, room.Id);
                }
                if (x == true)
                {
                    return room.Id;
                }
            }
            return null;
        }
        public string CheckOperation(DateTime dateStart,DateTime dateEnd)
        {
            bool x = true;
            foreach (var room in _validationManager.RoomManager.OperatingRooms)
            {
                foreach (var appointment in _validationManager.AppointmentManager.Appointment)
                {
                    x = CheckRoom(dateStart, dateEnd, room.Id);
                }

                if (x == true)
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
        public string GetValidPatientEmail()
        {
            string patientEmail;
            while (true)
            {
                Console.WriteLine("Enter the Email of the patient:");
                patientEmail = Console.ReadLine();
                if (!_validationManager.PatientManager.CheckEmail(patientEmail))
                {
                    Console.WriteLine("Entered mail doesn't exists, try again.");
                }
                else
                {
                    return patientEmail;
                }
            }
        }
        
        public bool CheckEmail(string email)
        {
            foreach (Doctor doctor in _validationManager.DoctorManager.Doctors)
            {
                if (email == doctor.email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}