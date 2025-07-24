﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Species;

namespace PetFamily.Domain.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public SpeciesId SpeciesId { get; }
    
    public BreedId BreedId { get; }

    public static Result<SpeciesBreed> Create(SpeciesId speciesId, BreedId breedId)
    {
        var result = new SpeciesBreed(speciesId, breedId);
        
        return Result.Success(result);
    }
}