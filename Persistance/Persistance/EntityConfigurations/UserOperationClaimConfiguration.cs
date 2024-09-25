using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            UserOperationClaim[] userOperationClaims = { new(1, 1, 1), new(2, 1, 2) };

            builder.HasOne(uoc => uoc.User)
            .WithMany(u => u.UserOperationClaims)
            .HasForeignKey(uoc => uoc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(userOperationClaims);
        }
    }
}
