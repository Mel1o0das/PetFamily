using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.GetVolunteer;

public class GetVolunteerByIdHandler
{
    
    private readonly IVolunteersRepository _volunteersRepository;

    public GetVolunteerByIdHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Volunteer, Error>> Handle(Guid id, CancellationToken cancellationToken)
    {
        var result = await _volunteersRepository.GetById(id, cancellationToken);

        if(result.IsFailure)
            return result.Error;
        
        return result.Value;
    }
}