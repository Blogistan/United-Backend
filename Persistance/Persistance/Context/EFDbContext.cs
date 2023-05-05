using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security.Claims;

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
        public DbSet<VideoContent> VideoContents { get; set; }

        protected IConfiguration Configuration { get; set; }
        protected IHttpContextAccessor httpContextAccessor { get; set; }

        public EFDbContext(DbContextOptions dbContextOptions,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            this.httpContextAccessor = httpContextAccessor;
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

            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userClaim = identity?.Claims;
            var userId = userClaim?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var item in entities)
            {
                switch (item.State)
                {
                    case EntityState.Deleted:
                        item.Entity.DeletedDate = DateTime.UtcNow;
                        item.Entity.CreateUser = int.Parse(userId);
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedDate = DateTime.UtcNow;
                        item.Entity.UpdateUser = int.Parse(userId);
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.UtcNow;
                        item.Entity.DeleteUser = int.Parse(userId);
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
