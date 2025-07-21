using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Domain.Pets;

public class Pet : Shared.Entity<PetId>
{
    // ef core
    private Pet(PetId id) 
        : base(id)
    {
    }

    private Pet(
        PetId id, 
        string name, 
        string description, 
        SpeciesBreed speciesBreed,
        string color,
        InformationAboutHealth informationAboutHealth,
        Address address,
        DateTime birthDate,
        DetailsForHelp detailsForHelp,
        PhoneNumber phoneNumber)
        : base(id)
    {
        Name = name;
        Description = description;
        SpeciesBreed = speciesBreed;
        Color = color;
        InformationAboutHealth = informationAboutHealth;
        Address = address;
        DateOfBirth = birthDate;
        DetailsForHelp = detailsForHelp;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public SpeciesBreed SpeciesBreed { get; private set; }
    
    public string Color { get; private set; }
    
    public InformationAboutHealth InformationAboutHealth { get; private set; }

    public Address Address { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    
    public DateTime DateOfBirth { get; private set; }

    public HelpStatus Status { get; private set; } = HelpStatus.NEEDS_HELP;
    
    public DetailsForHelp DetailsForHelp { get; private set; }
    
    public DateTime DateCreated { get; private set; } = DateTime.Now;

    public static Result<Pet, Error> Create(
        string name, 
        string description, 
        SpeciesBreed? speciesBreed,
        string color,
        InformationAboutHealth? informationAboutHealth,
        Address? address,
        DateTime birthDate,
        DetailsForHelp? detailsForHelp,
        PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("Pet name");
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid("Pet description");
        
        if (speciesBreed is null)
            return Errors.General.ValueIsInvalid("Species breed");
        
        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsInvalid("Pet color");
        
        if(informationAboutHealth is null)
            return Errors.General.ValueIsInvalid("Information about health");
        
        if (address is null)
            return Errors.General.ValueIsInvalid("Address");
        
        if (detailsForHelp is null)
            return Errors.General.ValueIsInvalid("Details for help");
        
        var pet = new Pet(
            PetId.NewPetId(), 
            name, 
            description, 
            speciesBreed, 
            color, 
            informationAboutHealth, 
            address, 
            birthDate, 
            detailsForHelp, 
            phoneNumber);
        
        return pet;
    }
}