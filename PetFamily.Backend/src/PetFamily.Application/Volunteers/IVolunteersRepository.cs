using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken);
    
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
}