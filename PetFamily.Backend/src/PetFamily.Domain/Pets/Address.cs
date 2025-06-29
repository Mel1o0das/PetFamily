using CSharpFunctionalExtensions;

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

    public static Result<Address> Create(string city, string street, int streetNumber)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>("City cannot be empty");
        
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>("Street cannot be empty");
        
        if (streetNumber <= 0)
            return Result.Failure<Address>("Street number cannot be zero or negative");
        
        return new Address(city, street, streetNumber);
    }
}