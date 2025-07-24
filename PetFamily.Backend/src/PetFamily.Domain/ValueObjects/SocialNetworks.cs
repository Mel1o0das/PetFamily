using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record SocialNetworks
{
    private SocialNetworks(string name, string link)
    {
        Name = name;
        Link = link;
    }
    
    public string Name { get; }
    
    public string Link { get; }

    public static Result<SocialNetworks, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("name");

        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("link");

        return new SocialNetworks(name, link);
    }
}