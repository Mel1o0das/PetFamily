using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;

namespace PetFamily.Domain.Volunteers;

public class Volunteer
{
    private readonly List<Pet> _pets = [];
    
    // ef core
    private Volunteer()
    {
    }

    private Volunteer(string description)
    {
        Description = description;
    }
    
    public Guid Id { get; private set; }
    
    public InformationAboutVolunteer InformationAboutVolunteer { get; private set; }
    
    public string Description { get; private set; }
    
    public int Experience { get; private set; }
    
    public int CountPetsWithHome => Pets.Select(p => p.Status == HelpStatus.FOUND_HOME).Count();

    public int CountPetsSearchHome => Pets.Select(p => p.Status == HelpStatus.LOOKING_HOME).Count();

    public int CountPetsNeedHelp => Pets.Select(p => p.Status == HelpStatus.NEEDS_HELP).Count();
    
    public SocialNetworks SocialNetworks { get; private set; }
    
    public DetailsForHelp DetailsForHelp { get; private set; }
    
    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer> Create(string description)
    {
        if(string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Invalid description.");
        
        var volunteer = new Volunteer(description);
        
        return Result.Success(volunteer);
    }
}