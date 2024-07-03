using Core.Security.Entities;
using MediatR;

namespace Application.Notifications.PasswordChangedNotification
{
    public class PasswordChangedNotification:INotification
    {
        public User User { get; set; }
    }
}
