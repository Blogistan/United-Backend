using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class ForgotPassword : Entity<int>
    {
        public int UserId { get; set; }
        public string ActivationKey { get; set; }
        public bool IsVerified { get; set; }
        public DateTime ExpireDate { get; set; }

        public byte[]? OldPasswordSalt { get; set; }
        public byte[]? OldPasswordHash { get; set; }

        public byte[]? NewPasswordSalt { get; set; }
        public byte[]? NewPasswordHash { get; set; }

        public virtual User User { get; set; } = null!;

        public ForgotPassword()
        {

        }
        public ForgotPassword(int userId, bool isVerified)
        {
            this.UserId = userId;
            this.IsVerified = isVerified;

        }
        public ForgotPassword(int id,int userId, bool isVerified):base(id)
        {
            this.UserId = userId;
            this.IsVerified = isVerified;

        }
    }
}
