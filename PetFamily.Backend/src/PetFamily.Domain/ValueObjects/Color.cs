using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Color
{
    private Color(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Color, Error> Create(string color)
    {
        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsInvalid(color);
        
        return new Color(color);
    }
}