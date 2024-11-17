using Core.Application.Dtos;
using Core.Security.Enums;

namespace Application.Features.SiteUsers.Dtos
{
    public record SiteUserListViewDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string Biography { get; set; }
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public AuthenticatorType AuthenticatorType { get; set; }

        public SiteUserListViewDto()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public SiteUserListViewDto(int id, string firstName, string lastName, string email, bool isVerified)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsVerified = isVerified;
        }
    }
}
