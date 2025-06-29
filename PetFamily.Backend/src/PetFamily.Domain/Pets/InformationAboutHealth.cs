using CSharpFunctionalExtensions;

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

    public static Result<InformationAboutHealth> Create(
        string description, 
        double weight, 
        double height,
        bool isVaccinated, 
        bool isCastrated)
    {
        if(weight <= 0)
            return Result.Failure<InformationAboutHealth>("Weight must be positive");
        
        if(height <= 0)
            return Result.Failure<InformationAboutHealth>("Height must be positive");
        
        return new InformationAboutHealth(description, weight, height, isVaccinated, isCastrated);
    }
}