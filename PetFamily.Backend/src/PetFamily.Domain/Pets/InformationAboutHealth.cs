using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pets;

public record InformationAboutHealth
{
    private InformationAboutHealth(string description, double weight, double height, bool isVaccinated, bool isCastrated)
    {
        Description = description;
        Weight = weight;
        Height = height;
        IsVaccinated = isVaccinated;
        IsCastrated = isCastrated;
    }
    
    public string Description { get; }
    
    public double Weight { get; }
    
    public double Height { get; }
    
    public bool IsVaccinated { get; }
    
    public bool IsCastrated { get; }

    public static Result<InformationAboutHealth, Error> Create(
        string description, 
        double weight, 
        double height,
        bool isVaccinated, 
        bool isCastrated)
    {
        if(weight <= 0)
            return Errors.General.ValueIsInvalid("weight");
        
        if(height <= 0)
            return Errors.General.ValueIsInvalid("height");
        
        return new InformationAboutHealth(description, weight, height, isVaccinated, isCastrated);
    }
}