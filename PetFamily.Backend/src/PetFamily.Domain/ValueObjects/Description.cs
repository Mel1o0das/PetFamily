using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Description
{
    private Description(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Description, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.Text.MAX_HIGH_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("description");
        
        return new Description(description);
    }
}