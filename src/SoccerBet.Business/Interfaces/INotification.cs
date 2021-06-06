using SoccerBet.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Interfaces
{
    public interface INotification
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
