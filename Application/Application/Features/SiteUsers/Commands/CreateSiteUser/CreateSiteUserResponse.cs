using Core.Application.Responses;

namespace Application.Features.SiteUsers.Commands.CreateSiteUser
{
    public class CreateSiteUserResponse : IResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public CreateSiteUserResponse()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public CreateSiteUserResponse(int id, string firstName, string lastName, string email, bool status)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = status;
        }
    }
}
