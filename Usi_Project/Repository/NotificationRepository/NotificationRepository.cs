using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Usi_Project.Repository;
using Usi_Project.Appointments;

namespace Usi_Project.Repository.NotificationRepository
{
    public class NotificationRepository
    {
        private string _notificationFilename;
        private List<Notification> _notifications;
        private Factory _manager;
        
        public NotificationRepository(string notificationFilename, Factory manager)
        {
            _notificationFilename = notificationFilename;
            _manager = manager;
        }

        public NotificationRepository()
        {
        }

        public string NotificationFilename
        {
            get => _notificationFilename;
            set => _notificationFilename = value;
        }

        public List<Notification> Notifications
        {
            get => _notifications;
            set => _notifications = value;
        }

        public Factory Manager
        {
            get => _manager;
            set => _manager = value;
        }


        public void LoadData()
        {
            JsonSerializerSettings json = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            _notifications = JsonConvert.DeserializeObject<List<Notification>>(File.ReadAllText(_notificationFilename), json);

        }
        public void serialize()
        {
            using (StreamWriter file = File.CreateText(_notificationFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, _manager);
            }
        }
    }
}