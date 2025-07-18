using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Email
{
    private Email(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; }

    public static Result<Email, Error> Create(string emailAddress)
    {
        var email = new Regex(Constants.Email.PATTERN);
        
        if(!email.IsMatch(emailAddress))
            return Errors.General.ValueIsInvalid("email");
        
        return new Email(emailAddress);
    }
}