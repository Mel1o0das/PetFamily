using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    // ef core
    private Volunteer(VolunteerId id)
        : base(id)
    {
    }

    private Volunteer(
        VolunteerId id, 
        InformationAboutVolunteer informationAboutVolunteer, 
        PhoneNumber phoneNumber,
        Email email,
        string description, 
        DetailsForHelp detailsForHelp)
        : base(id)
    {
        InformationAboutVolunteer = informationAboutVolunteer;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
        DetailsForHelp = detailsForHelp;
    }
    
    public InformationAboutVolunteer InformationAboutVolunteer { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    
    public Email Email { get; private set; }
    
    public string Description { get; private set; }

    public SocialNetworksDetails? SocialNetworksDetails { get; private set; }
    
    public DetailsForHelp DetailsForHelp { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    
    public int CountPetsWithHome => _pets.Select(p => p.Status == HelpStatus.FOUND_HOME).Count();

    public int CountPetsSearchHome => _pets.Select(p => p.Status == HelpStatus.LOOKING_HOME).Count();

    public int CountPetsNeedHelp => _pets.Select(p => p.Status == HelpStatus.NEEDS_HELP).Count();

    public static Result<Volunteer, Error> Create(
        InformationAboutVolunteer? informationAboutVolunteer, 
        PhoneNumber phoneNumber,
        Email email,
        string description, 
        DetailsForHelp? detailsForHelp)
    {
        if(informationAboutVolunteer is null)
            return Errors.General.ValueIsInvalid("informationAboutVolunteer");

        if(string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid("description");
        
        if(detailsForHelp is null)
            return Errors.General.ValueIsInvalid("detailsForHelp");
        
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), informationAboutVolunteer, phoneNumber, email, description, detailsForHelp);
        
        return volunteer;
    }
}