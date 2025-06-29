using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Presistence.EntitesConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.Property(x => x.ApartmentNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.BuildingNumber)
            .IsRequired()
            .HasMaxLength(20);

    }
}
