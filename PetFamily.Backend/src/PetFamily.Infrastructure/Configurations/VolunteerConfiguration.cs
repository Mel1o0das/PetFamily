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

        builder.OwnsMany(v => v.DetailsForHelp, vb =>
        {
            vb.ToJson("details_for_help");

            vb.Property(r => r.Requisites)
                .IsRequired()
                .HasColumnName("requisite");
            vb.Property(r => r.Description)
                .IsRequired()
                .HasColumnName("description");
        });

        builder.OwnsMany(v => v.SocialNetworks, snb =>
        {
            snb.ToJson("social_networks");
            
            snb.Property(sn => sn.Name)
                .IsRequired()
                .HasColumnName("social_network_name");
            snb.Property(sn => sn.Link)
                .IsRequired()
                .HasColumnName("social_network_link");
        });
        
        builder
            .HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}