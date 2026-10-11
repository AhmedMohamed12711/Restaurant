
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssemply = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(applicationAssemply));
        services.AddAutoMapper(applicationAssemply);

        services.AddValidatorsFromAssembly(applicationAssemply)
            .AddFluentValidationAutoValidation();
    }
}