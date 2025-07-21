using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;

public class Breed : Shared.Entity<BreedId>
{
    // ef core
    private Breed(BreedId id)
        : base(id)
    {
    }

    private Breed(BreedId id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<Breed, Error> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("breed name");

        var breed = new Breed(BreedId.NewBreedId(), name);
        
        return breed;
    }
}