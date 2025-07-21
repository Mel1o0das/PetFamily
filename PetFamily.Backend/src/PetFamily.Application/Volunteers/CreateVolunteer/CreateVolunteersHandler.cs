using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteersHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteersHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken)
    {
        var informationAboutVolunteerResult = InformationAboutVolunteer.Create(
            request.Surname,
            request.Name,
            request.Patronymic,
            request.Experience);
        
        if(informationAboutVolunteerResult.IsFailure)
            return informationAboutVolunteerResult.Error;
        
        var emailResult = Email.Create(request.Email);
        if(emailResult.IsFailure)
            return emailResult.Error;
        
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        if(phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var detailsForHelpResult = DetailsForHelp.Create(
            request.Requisites,
            request.DescriptionForHelp);
        
        if(detailsForHelpResult.IsFailure)
            return detailsForHelpResult.Error;
        
        var volunteer = Volunteer.Create(
            informationAboutVolunteerResult.Value,
            phoneNumberResult.Value,
            emailResult.Value,
            request.Description,
            detailsForHelpResult.Value);
        
        if(volunteer.IsFailure)
            return volunteer.Error;
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);

        return (Guid)volunteer.Value.Id;
    }
}