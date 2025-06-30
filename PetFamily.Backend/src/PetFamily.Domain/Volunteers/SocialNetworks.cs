using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record SocialNetworks
{
    private SocialNetworks(string name, string link)
    {
        Name = name;
        Link = link;
    }
    
    public string Name { get; }
    
    public string Link { get; }

    public static Result<SocialNetworks> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetworks>("Name cannot be empty");

        if (string.IsNullOrWhiteSpace(link))
            return Result.Failure<SocialNetworks>("Link cannot be empty");

        return new SocialNetworks(name, link);
    }
}