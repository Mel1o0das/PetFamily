using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    
    // ef core
    private Species(SpeciesId id) 
        : base(id)
    {
    }

    private Species(SpeciesId id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }
    
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Name cannot be empty");
        
        var species = new Species(SpeciesId.NewSpeciesId(), name);
        
        return Result.Success(species);
    }
}