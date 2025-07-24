using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    
    // ef core
    private Species(SpeciesId id) 
        : base(id)
    {
    }

    private Species(SpeciesId id, Name name)
        : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }
    
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species, Error> Create(Name name)
    {
        var species = new Species(SpeciesId.NewSpeciesId(), name);
        
        return species;
    }
}