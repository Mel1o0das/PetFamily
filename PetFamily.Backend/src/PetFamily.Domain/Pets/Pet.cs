using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pets;

public class Pet
{
    //ef core
    private Pet()
    {
    }

    private Pet(string name, string description, string breed, string color, string phoneNumber)
    {
        Name = name;
        Description = description;
        Breed = breed;
        Color = color;
        PhoneNumber = phoneNumber;
    }
    
    public Guid Id { get; private set; }

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
        
        var pet = new Pet(name, description, breed, color, phoneNumber);
        
        return Result.Success(pet);
    }
}