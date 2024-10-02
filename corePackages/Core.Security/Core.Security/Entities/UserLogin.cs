using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class UserLogin : Entity<int>
    {
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public UserLogin()
        {

        }
        public UserLogin(string providerName, string providerKey, string providerDisplayName, int userID)
        {
            this.ProviderName = providerName;
            this.ProviderKey = providerKey;
            this.ProviderDisplayName = providerDisplayName;
            this.UserId = userID;
        }
        public UserLogin(int id, string providerName, string providerKey, string providerDisplayName, int userID) : base(id)
        {
            this.ProviderName = providerName;
            this.ProviderKey = providerKey;
            this.ProviderDisplayName = providerDisplayName;
            this.UserId = userID;
        }
    }
}
