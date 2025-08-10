using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteersHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteersHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerRequest> validator)
    {
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var informationAboutVolunteer = InformationAboutVolunteer.Create(
            request.Surname,
            request.Name,
            request.Patronymic,
            request.Experience).Value;
        
        var email = Email.Create(request.Email).Value;
        
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        
        var detailsForHelpList = new List<DetailsForHelp>();
        var detailsForHelpResult = request.DetailsForHelp
           .Select(d => DetailsForHelp.Create(d.Requisites, d.Description));

        foreach (var detailsForHelp in detailsForHelpResult) 
        {
           if (detailsForHelp.IsFailure)
            return detailsForHelp.Error;
           
           detailsForHelpList.Add(detailsForHelp.Value);
        }
        
        var description = Description.Create(request.Description).Value;
        
        var socialNetworksList = new List<SocialNetworks>();
        var socialNetworksResult = request.SocialNetworks
            .Select(s => SocialNetworks.Create(s.Name, s.Link));

        foreach (var socialNetwork in socialNetworksResult)
        {
            if(socialNetwork.IsFailure)
                return socialNetwork.Error;
            socialNetworksList.Add(socialNetwork.Value);
        }

        var volunteer = new Volunteer(
            volunteerId,
            informationAboutVolunteer,
            phoneNumber,
            email,
            description,
            detailsForHelpList,
            socialNetworksList);
        
        await _volunteersRepository.Add(volunteer, cancellationToken);

        return volunteerId.Value;
    }
}