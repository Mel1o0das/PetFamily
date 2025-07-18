using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record InformationAboutVolunteer
{
    private InformationAboutVolunteer(string surname, string name, string patronymic, int experience)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Experience = experience;
    }
    
    public string Surname { get; }
    
    public string Name { get; }
    
    public string Patronymic { get; }
    
    public int Experience { get; }

    public static Result<InformationAboutVolunteer, Error> Create(
        string surname, 
        string name, 
        string patronymic, 
        int experience)
    {
        if (string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValueIsRequired("surname");
        
        if(string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("name");
        
        if(string.IsNullOrWhiteSpace(patronymic))
            return Errors.General.ValueIsRequired("patronymic");
        
        if(experience < 0)
            return Errors.General.ValueIsInvalid("experience");
        
        return new InformationAboutVolunteer(surname, name, patronymic, experience);
    }
}