using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Name
{
    private Name(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Name, Error> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name) || name.Length > Constants.Text.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("name");
        
        return new Name(name);
    }
}