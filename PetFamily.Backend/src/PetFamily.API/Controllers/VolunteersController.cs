using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.GetVolunteer;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Volunteer>> GetById(
        [FromServices] GetVolunteerByIdHandler handler,
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(id, cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteersHandler handler,
        [FromServices] IValidator<CreateVolunteerRequest> validator,
        [FromBody] CreateVolunteerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors;

            List<ResponseError> errors = [];
            
            foreach (var validationError in validationErrors)
            {
                var error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage);
                
                var responseError = new ResponseError(
                    error.Code, 
                    error.Message, 
                    validationError.PropertyName);
                
                errors.Add(responseError);
            }

            var envelope = Envelope.Error(errors);
            
            return BadRequest(envelope);
        }
        
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}