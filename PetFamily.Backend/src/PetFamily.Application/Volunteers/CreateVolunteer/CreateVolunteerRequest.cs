using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string Surname,
    string Name,
    string Patronymic,
    string Email,
    string PhoneNumber,
    int Experience,
    string Description,
    IEnumerable<DetailsForHelpDto> DetailsForHelp,
    IEnumerable<SocialNetworksDto> SocialNetworks);