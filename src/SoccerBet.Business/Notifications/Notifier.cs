using SoccerBet.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SoccerBet.Business.Notifications
{
    public class Notifier : INotification
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }
        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
