

using HouseRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Data.Configurations
{
    public class HouseEntityConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<House> builder)
        {
            builder
               .Property(h => h.CreatedOn)
               .HasDefaultValueSql("GETDATE()");

            builder .Property(h => h.isActive)
                .HasDefaultValue(true);

            builder
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Agent)
                .WithMany(a => a.ManagedHouses)
                .HasForeignKey(h => h.AgentId) 
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Renter)
                .WithMany(r => r.RentedHouses)
                .HasForeignKey(h => h.RenterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
