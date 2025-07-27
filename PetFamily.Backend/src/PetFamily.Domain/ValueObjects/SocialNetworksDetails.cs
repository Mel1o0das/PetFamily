namespace PetFamily.Domain.ValueObjects;

public record SocialNetworksDetails
{
    private SocialNetworksDetails()
    {
    }

    public SocialNetworksDetails(IEnumerable<SocialNetworks> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }

    public IReadOnlyList<SocialNetworks> SocialNetworks { get; } = [];
}