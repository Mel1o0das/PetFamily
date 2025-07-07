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
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

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
        
        builder.Property(p => p.Color).IsRequired();

        builder.ComplexProperty(p => p.InformationAboutHealth, pb =>
        {
            pb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
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
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");
            
            pb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");
            
            pb.Property(a => a.StreetNumber)
                .IsRequired()
                .HasColumnName("street_number");
        });
        
        builder.Property(p => p.PhoneNumber).IsRequired();
        
        builder.Property(p => p.DateOfBirth).IsRequired();
        
        builder.Property(p => p.Status).IsRequired();

        builder.ComplexProperty(p => p.DetailsForHelp, pb =>
        {
            pb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            
            pb.Property(p => p.Requisites).IsRequired();
        });
        
        builder.Property(p => p.DateCreated).IsRequired();
    }
}