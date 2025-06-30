﻿namespace PetFamily.Domain.Pets;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static PetId NewPetId() => new PetId(Guid.NewGuid());
    
    public static PetId EmptyPetId() => new PetId(Guid.Empty);
}