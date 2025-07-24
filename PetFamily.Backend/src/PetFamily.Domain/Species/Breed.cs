using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Species;

public class Breed : Shared.Entity<BreedId>
{
    // ef core
    private Breed(BreedId id)
        : base(id)
    {
    }

    private Breed(BreedId id, Name name)
        : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public static Result<Breed, Error> Create(Name name)
    {
        var breed = new Breed(BreedId.NewBreedId(), name);
        
        return breed;
    }
}