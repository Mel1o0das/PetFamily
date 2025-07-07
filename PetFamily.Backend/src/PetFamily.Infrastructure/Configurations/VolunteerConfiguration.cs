using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.InformationAboutVolunteer, vb =>
        {
            vb.Property(v => v.Surname).IsRequired();
            vb.Property(v => v.Name).IsRequired();
            vb.Property(v => v.Patronymic).IsRequired();
            vb.Property(v => v.Email).IsRequired();
            vb.Property(v => v.PhoneNumber).IsRequired();
            vb.Property(v => v.Experience).IsRequired();
        });
        
        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

        builder.OwnsOne(
            v => v.SocialNetworksDetails,
            vb =>
            {
                vb.ToJson();
                vb.OwnsMany(sn => sn.SocialNetworks, sb =>
                {
                    sb.Property(sn => sn.Name)
                        .IsRequired();
                    sb.Property(sn => sn.Link)
                        .IsRequired();
                });
            });

        builder.ComplexProperty(v => v.DetailsForHelp, vb =>
        {
            vb.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            
            vb.Property(v => v.Requisites).IsRequired();
        });

        builder
            .HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}