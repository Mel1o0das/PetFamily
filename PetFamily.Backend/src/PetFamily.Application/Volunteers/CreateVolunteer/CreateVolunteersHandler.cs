using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
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
        var volunteerId = VolunteerId.NewVolunteerId();
        
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
        
        var detailsForHelpList = new List<DetailsForHelp>();
        var detailsForHelpResult = request.DetailsForHelp
           .Select(d => DetailsForHelp.Create(d.Requisites, d.Description));

        foreach (var detailsForHelp in detailsForHelpResult) 
        {
           if (detailsForHelp.IsFailure)
            return detailsForHelp.Error;
           
           detailsForHelpList.Add(detailsForHelp.Value);
        }
        
        var descriptionResult = Description.Create(request.Description);
        if(descriptionResult.IsFailure)
            return descriptionResult.Error;

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
            informationAboutVolunteerResult.Value,
            phoneNumberResult.Value,
            emailResult.Value,
            descriptionResult.Value,
            new DetailsForHelpList(detailsForHelpList),
            new SocialNetworksDetails(socialNetworksList));
        
        await _volunteersRepository.Add(volunteer, cancellationToken);

        return volunteerId.Value;
    }
}