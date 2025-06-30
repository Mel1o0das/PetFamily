using CSharpFunctionalExtensions;

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

    public static Result<DetailsForHelp> Create(string requisites, string description)
    {
        if (string.IsNullOrWhiteSpace(requisites))
            return Result.Failure<DetailsForHelp>("Required parameters are required");
        
        return new DetailsForHelp(requisites, description);
    }
}