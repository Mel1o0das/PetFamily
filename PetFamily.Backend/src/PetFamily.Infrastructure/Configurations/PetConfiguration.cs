using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.ComplexProperty(p => p.Name, pn =>
        {
            pn.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(Constants.Text.MAX_LOW_TEXT_LENGTH);
        });
        
        builder.ComplexProperty(p => p.Description, pd =>
        {
            pd.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(Constants.Text.MAX_HIGH_TEXT_LENGTH);
        });

        builder.OwnsOne(p => p.SpeciesBreed, pb =>
        {
            pb.Property(p => p.BreedId)
                .HasConversion(
                    id => id.Value,
                    value => BreedId.Create(value))
                .HasColumnName("breed_id");
                
            pb.Property(p => p.SpeciesId)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");
        });

        builder.ComplexProperty(p => p.Color, pc =>
        {
            pc.Property(c => c.Value)
                .IsRequired()
                .HasColumnName("color");
        });

        builder.ComplexProperty(p => p.InformationAboutHealth, pb =>
        {
            pb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.Text.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("description");

            pb.Property(p => p.Height).IsRequired().HasColumnName("height");
            pb.Property(p => p.Weight).IsRequired().HasColumnName("weight");
            pb.Property(p => p.IsCastrated).IsRequired().HasColumnName("is_castrated");
            pb.Property(p => p.IsVaccinated).IsRequired().HasColumnName("is_vaccinated");
        });

        builder.ComplexProperty(p => p.Address, pb =>
        {
            pb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.Text.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");
            
            pb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.Text.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");
            
            pb.Property(a => a.StreetNumber)
                .IsRequired()
                .HasColumnName("street_number");
        });
        
        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(p => p.Number).IsRequired().HasColumnName("phone_number");
        });
        
        builder.Property(p => p.DateOfBirth).IsRequired();
        
        builder.Property(p => p.Status).IsRequired();

        builder.ComplexProperty(p => p.DetailsForHelp, pb =>
        {
            pb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.Text.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("details_for_help_description");
            
            pb.Property(p => p.Requisites)
                .IsRequired()
                .HasColumnName("details_for_help_requisites");
        });
        
        builder.Property(p => p.DateCreated).IsRequired();
    }
}