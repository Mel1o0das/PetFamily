using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Volunteers;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = default)
    {
        //var result = await 
        
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteersHandler handler,
        [FromBody] CreateVolunteerRequest volunteer, 
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(volunteer, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}