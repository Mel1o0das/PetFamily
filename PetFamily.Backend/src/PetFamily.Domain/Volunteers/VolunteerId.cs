﻿namespace PetFamily.Domain.Volunteers;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static VolunteerId NewVolunteerId() => new VolunteerId(Guid.NewGuid());
    
    public static VolunteerId EmptyVolunteerId() => new VolunteerId(Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);
    
    public static implicit operator Guid(VolunteerId volunteerId)
    { 
        ArgumentNullException.ThrowIfNull(volunteerId);
        
        return volunteerId.Value;
    }
}