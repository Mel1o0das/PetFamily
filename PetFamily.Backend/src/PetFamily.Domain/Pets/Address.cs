using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pets;

public record Address
{
    private Address(string city, string street, int streetNumber)
    {
        City = city;
        Street = street;
        StreetNumber = streetNumber;
    }
    
    public string City { get; }
    
    public string Street { get; }
    
    public int StreetNumber { get; }

    public static Result<Address, Error> Create(string city, string street, int streetNumber)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInvalid("city");
        
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInvalid("street");
        
        if (streetNumber <= 0)
            return Errors.General.ValueIsInvalid("streetNumber");
        
        return new Address(city, street, streetNumber);
    }
}