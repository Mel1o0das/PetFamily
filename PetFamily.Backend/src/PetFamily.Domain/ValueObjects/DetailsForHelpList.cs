namespace PetFamily.Domain.ValueObjects;

public record DetailsForHelpList
{
    private DetailsForHelpList()
    {
    }

    public DetailsForHelpList(IEnumerable<DetailsForHelp> detailsForHelp)
    {
        DetailsForHelp = detailsForHelp.ToList();
    }

    public IReadOnlyList<DetailsForHelp> DetailsForHelp { get; } = [];
}