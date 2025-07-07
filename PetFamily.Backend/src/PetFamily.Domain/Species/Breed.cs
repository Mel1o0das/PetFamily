using CSharpFunctionalExtensions;

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

    public static Result<Breed> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Failure<Breed>("Name cannot be empty");

        var breed = new Breed(BreedId.NewBreedId(), name);
        
        return Result.Success(breed);
    }
}