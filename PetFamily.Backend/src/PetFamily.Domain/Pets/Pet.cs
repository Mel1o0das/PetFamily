using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pets;

public class Pet : Shared.Entity<PetId>
{
    // ef core
    private Pet(PetId id) 
        : base(id)
    {
    }

    private Pet(PetId id, string name, string description, string breed, string color, string phoneNumber)
        : base(id)
    {
        Name = name;
        Description = description;
        Breed = breed;
        Color = color;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; }
    
    public PetType Type { get; private set; }
    
    public string Description { get; private set; }
    
    public string Breed { get; private set; }
    
    public string Color { get; private set; }
    
    public InformationAboutHealth? InformationAboutHealth { get; private set; }

    public Address Address { get; private set; }
    
    public string PhoneNumber { get; private set; }
    
    public DateTime DateOfBirth { get; private set; }

    public HelpStatus Status { get; private set; } = HelpStatus.NEEDS_HELP;
    
    public DetailsForHelp? DetailsForHelp { get; private set; }
    
    public DateTime DateCreated { get; private set; } = DateTime.Now;

    public static Result<Pet> Create(string name, string description, string breed, string color, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Pet>("Name is required");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Pet>("Description is required");
        
        if (string.IsNullOrWhiteSpace(breed))
            return Result.Failure<Pet>("Breed is required");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<Pet>("Color is required");
        
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Pet>("Phone number is required");
        
        var pet = new Pet(PetId.NewPetId(), name, description, breed, color, phoneNumber);
        
        return Result.Success(pet);
    }
}