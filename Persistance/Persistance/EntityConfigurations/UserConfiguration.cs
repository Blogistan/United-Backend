using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.FirstName).HasColumnName("FirstName");
            builder.Property(u => u.LastName).HasColumnName("LastName");
            builder.Property(u => u.Email).HasColumnName("Email");
            builder.HasIndex(indexExpression: u => u.Email, name: "UK_Users_Email").IsUnique();
            builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
            builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(u => u.IsActive).HasColumnName("IsActive").HasDefaultValue(false);
            builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");

            byte[] hash, salt;
            HashingHelper.CreatePasswordHash("Admin123", out hash, out salt);
            User[] users =
            {
                new(1,"Admin","Admin","esquetta@gmail.com",salt,hash,true,Core.Security.Enums.AuthenticatorType.None)
            };

            builder.HasData(users);
        }
    }
}
