using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
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
        Name name, 
        Description description, 
        SpeciesBreed speciesBreed,
        Color color,
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

    public Name Name { get; private set; }
    
    public Description Description { get; private set; }
    
    public SpeciesBreed SpeciesBreed { get; private set; }
    
    public Color Color { get; private set; }
    
    public InformationAboutHealth InformationAboutHealth { get; private set; }

    public Address Address { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    
    public DateTime DateOfBirth { get; private set; }

    public HelpStatus Status { get; private set; } = HelpStatus.NEEDS_HELP;
    
    public DetailsForHelp DetailsForHelp { get; private set; }
    
    public DateTime DateCreated { get; private set; } = DateTime.Now;

    public static Result<Pet, Error> Create(
        Name name, 
        Description description, 
        SpeciesBreed speciesBreed,
        Color color,
        InformationAboutHealth informationAboutHealth,
        Address address,
        DateTime birthDate,
        DetailsForHelp detailsForHelp,
        PhoneNumber phoneNumber)
    {
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