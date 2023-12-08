﻿using Microsoft.Extensions.DependencyInjection;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Services;


namespace Vezeeta.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
