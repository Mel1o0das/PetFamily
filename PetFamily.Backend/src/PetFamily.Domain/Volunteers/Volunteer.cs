using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private readonly List<SocialNetworks> _socialNetworks = [];
    private readonly List<DetailsForHelp> _detailsForHelp = [];

    // ef core
    private Volunteer(VolunteerId id)
        : base(id)
    {
    }

    public Volunteer(
        VolunteerId id, 
        InformationAboutVolunteer informationAboutVolunteer, 
        PhoneNumber phoneNumber,
        Email email,
        Description description, 
        IEnumerable<DetailsForHelp>? detailsForHelp,
        IEnumerable<SocialNetworks>? socialNetworks)
        : base(id)
    {
        InformationAboutVolunteer = informationAboutVolunteer;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
        _detailsForHelp = detailsForHelp!.ToList();
        _socialNetworks = socialNetworks!.ToList();
    }
    
    public InformationAboutVolunteer InformationAboutVolunteer { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    
    public Email Email { get; private set; }
    
    public Description Description { get; private set; }
    
    public IReadOnlyList<SocialNetworks>? SocialNetworks => _socialNetworks;

    public IReadOnlyList<DetailsForHelp>? DetailsForHelp => _detailsForHelp;

    public IReadOnlyList<Pet> Pets => _pets;
    
    public int CountPetsWithHome => _pets.Select(p => p.Status == HelpStatus.FOUND_HOME).Count();

    public int CountPetsSearchHome => _pets.Select(p => p.Status == HelpStatus.LOOKING_HOME).Count();

    public int CountPetsNeedHelp => _pets.Select(p => p.Status == HelpStatus.NEEDS_HELP).Count();
}