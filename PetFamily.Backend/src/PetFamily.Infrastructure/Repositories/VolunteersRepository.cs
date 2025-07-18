using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .ThenInclude(p => p.SpeciesBreed)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);
        
        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);
        
        return volunteer;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
       
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }
}