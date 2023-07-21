using Domain.Entities;
using MediatR;

namespace Application.Notifications.RegisterNotification
{
    public class RegisteredNotification : INotification
    {
        public SiteUser SiteUser { get; set; }
    }
}
