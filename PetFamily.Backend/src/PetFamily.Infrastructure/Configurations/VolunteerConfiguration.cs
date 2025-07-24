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
            vb.Property(v => v.Surname).IsRequired().HasColumnName("volunteer_surname");
            vb.Property(v => v.Name).IsRequired().HasColumnName("volunteer_name");
            vb.Property(v => v.Patronymic).IsRequired().HasColumnName("volunteer_patronymic");
            vb.Property(v => v.Experience).IsRequired().HasColumnName("volunteer_experience");
        });
        
        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(e => e.EmailAddress).IsRequired().HasColumnName("volunteer_email");
        });
        
        builder.ComplexProperty(v => v.PhoneNumber, pn =>
        {
            pn.Property(n => n.Number).IsRequired().HasColumnName("volunteer_phone_number");
        });
        
        builder.ComplexProperty(v => v.Description, vd =>
        {
            vd.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("volunteer_description")
                .HasMaxLength(Constants.Text.MAX_HIGH_TEXT_LENGTH);
        });

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
                .HasMaxLength(Constants.Text.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("details_for_help_description");
            
            vb.Property(v => v.Requisites)
                .IsRequired()
                .HasColumnName("details_for_help_requisites");
        });

        builder
            .HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}