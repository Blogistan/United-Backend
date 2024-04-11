using Core.Application.Dtos;

namespace Application.Features.SiteUsers.Dtos
{
    public class SiteUserListViewDto:IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public SiteUserListViewDto()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public SiteUserListViewDto(int id, string firstName, string lastName, string email, bool status)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = status;
        }
    }
}
