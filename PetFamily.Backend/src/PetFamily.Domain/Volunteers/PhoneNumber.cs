using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public class PhoneNumber
{
    private PhoneNumber(string number)
    {
        Number = number;
    }
    
    public string Number { get; }

    public static Result<PhoneNumber, Error> Create(string phoneNumber)
    {
        var regex = new Regex(Constants.PhoneNumber.PATTERN);
        if(!regex.IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid("phoneNumber");
        
        return new PhoneNumber(phoneNumber);
    }
}