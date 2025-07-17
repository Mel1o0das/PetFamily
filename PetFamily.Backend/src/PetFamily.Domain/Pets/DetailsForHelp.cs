using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pets;

public record DetailsForHelp
{
    private DetailsForHelp(string requisites, string description)
    {
        Requisites = requisites;
        Description = description;
    }
    
    public string Requisites { get; }
    
    public string Description { get; }

    public static Result<DetailsForHelp, Error> Create(string requisites, string description)
    {
        if (string.IsNullOrWhiteSpace(requisites))
            return Errors.General.ValueIsRequired("requisites");
        
        return new DetailsForHelp(requisites, description);
    }
}