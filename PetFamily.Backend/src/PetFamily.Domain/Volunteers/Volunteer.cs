using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;

namespace PetFamily.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private readonly List<SocialNetworks> _socialNetworks = [];

    // ef core
    private Volunteer(VolunteerId id)
        : base(id)
    {
    }

    private Volunteer(
        VolunteerId id, 
        InformationAboutVolunteer informationAboutVolunteer, 
        string description, 
        DetailsForHelp detailsForHelp)
        : base(id)
    {
        InformationAboutVolunteer = informationAboutVolunteer;
        Description = description;
        DetailsForHelp = detailsForHelp;
    }
    
    public InformationAboutVolunteer InformationAboutVolunteer { get; private set; }
    
    public string Description { get; private set; }

    public IReadOnlyList<SocialNetworks> SocialNetworks => _socialNetworks;
    
    public DetailsForHelp DetailsForHelp { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    
    public int CountPetsWithHome => _pets.Select(p => p.Status == HelpStatus.FOUND_HOME).Count();

    public int CountPetsSearchHome => _pets.Select(p => p.Status == HelpStatus.LOOKING_HOME).Count();

    public int CountPetsNeedHelp => _pets.Select(p => p.Status == HelpStatus.NEEDS_HELP).Count();

    public static Result<Volunteer> Create(
        InformationAboutVolunteer? informationAboutVolunteer, 
        string description, 
        DetailsForHelp? detailsForHelp)
    {
        if(informationAboutVolunteer is null)
            return Result.Failure<Volunteer>("Information about volunteer cannot be null.");

        if(string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Invalid description.");
        
        if(detailsForHelp is null)
            return Result.Failure<Volunteer>("Invalid details for volunteer.");
        
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), informationAboutVolunteer, description, detailsForHelp);
        
        return Result.Success(volunteer);
    }
}