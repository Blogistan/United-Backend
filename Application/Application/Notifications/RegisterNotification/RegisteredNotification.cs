using Core.Security.Entities;
using Domain.Entities;
using MediatR;

namespace Application.Notifications.RegisterNotification
{
    public class RegisteredNotification : INotification
    {
        public User SiteUser { get; set; }
    }
}
