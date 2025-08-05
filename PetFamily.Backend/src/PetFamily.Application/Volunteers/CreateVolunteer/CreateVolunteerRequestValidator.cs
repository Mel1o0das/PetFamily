using FluentValidation;
using PetFamily.Application.Valodation;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => 
            new { c.Surname, c.Name, c.Patronymic, c.Experience } ).MustBeValueObject(
            i => InformationAboutVolunteer.Create(
                i.Surname, i.Name, i.Patronymic, i.Experience));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleForEach(c => c.DetailsForHelp).MustBeValueObject(
            d => DetailsForHelp.Create(d.Requisites, d.Description));

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(
            s => SocialNetworks.Create(s.Name, s.Link));
    }
}