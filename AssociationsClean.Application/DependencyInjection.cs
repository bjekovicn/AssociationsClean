﻿

using AssociationsClean.Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace AssociationsClean.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                //ADD BEHAVIORS
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });



            return services;
        }
    }
}
