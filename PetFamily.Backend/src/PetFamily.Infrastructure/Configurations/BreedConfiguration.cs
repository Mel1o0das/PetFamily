﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        
        builder.ComplexProperty(b => b.Name, bn =>
        {
            bn.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(Constants.Text.MAX_LOW_TEXT_LENGTH);
        });
    }
}