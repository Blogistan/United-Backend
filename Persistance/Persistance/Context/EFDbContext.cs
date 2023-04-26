using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Persistance.Context
{
    public class EFDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
        public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }

        protected IConfiguration Configuration { get; set; }

        public EFDbContext(DbContextOptions dbContextOptions,
            IConfiguration configuration) : base(dbContextOptions)
        {
            this.Configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DbName")));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<Entity<int>>> entities = ChangeTracker.Entries<Entity<int>>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            foreach (var item in entities)
            {
                switch (item.State)
                {
                    case EntityState.Deleted:
                        item.Entity.DeletedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
