using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;

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
            pb.Property(p => p.BreedId).IsRequired().HasColumnName("breed_id");
            pb.Property(p => p.SpeciesId).IsRequired().HasColumnName("species_id");
        });
        
        builder.Property(p => p.Color).IsRequired();

        builder.ComplexProperty(p => p.InformationAboutHealth, pb =>
        {
            pb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            pb.Property(p => p.Height).IsRequired();
            pb.Property(p => p.Weight).IsRequired();
            pb.Property(p => p.IsCastrated).IsRequired();
            pb.Property(p => p.IsVaccinated).IsRequired();
        });

        builder.ComplexProperty(p => p.Address, pb =>
        {
            pb.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            
            pb.Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            
            pb.Property(p => p.StreetNumber).IsRequired();
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