using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record InformationAboutVolunteer
{
    private InformationAboutVolunteer(string surname, string name, string patronymic, string email, string phoneNumber, int experience)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Email = email;
        PhoneNumber = phoneNumber;
        Experience = experience;
    }
    
    public string Surname { get; }
    
    public string Name { get; }
    
    public string Patronymic { get; }
    
    public string Email { get; }
    
    public string PhoneNumber { get; }
    
    public int Experience { get; }

    public static Result<InformationAboutVolunteer> Create(
        string surname, 
        string name, 
        string patronymic, 
        string email,
        string phoneNumber,
        int experience)
    {
        if (string.IsNullOrWhiteSpace(surname))
            return Result.Failure<InformationAboutVolunteer>("Surname is required");
        
        if(string.IsNullOrWhiteSpace(name))
            return Result.Failure<InformationAboutVolunteer>("Name is required");
        
        if(string.IsNullOrWhiteSpace(patronymic))
            return Result.Failure<InformationAboutVolunteer>("Patronymic is required");
        
        if(string.IsNullOrWhiteSpace(email))
            return Result.Failure<InformationAboutVolunteer>("Email is required");
        
        if(string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<InformationAboutVolunteer>("Phone number is required");
        
        if(experience < 0)
            return Result.Failure<InformationAboutVolunteer>("Experience cannot be negative");
        
        return new InformationAboutVolunteer(surname, name, patronymic, email, phoneNumber, experience);
    }
}