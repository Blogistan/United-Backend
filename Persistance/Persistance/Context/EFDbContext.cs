using Core.Persistence.Repositories;
using Core.Security.Entities;
using Core.Security.Extensions;
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
            var userId = httpContextAccessor.HttpContext.User.GetUserId();

            foreach (var item in entities)
            {
                switch (item.State)
                {
                    case EntityState.Deleted:
                        item.Entity.DeletedDate = DateTime.UtcNow;
                        item.Entity.CreateUser = userId;
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedDate = DateTime.UtcNow;
                        item.Entity.UpdateUser = userId;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.UtcNow;
                        item.Entity.DeleteUser = userId;
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
