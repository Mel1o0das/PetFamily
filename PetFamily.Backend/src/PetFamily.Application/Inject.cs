﻿using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.GetVolunteer;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteersHandler>();
        services.AddScoped<GetVolunteerByIdHandler>();
        
        return services;
    }
}