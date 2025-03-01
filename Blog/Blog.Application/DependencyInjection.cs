using Blog.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // automatyczna rejestracja wszystkich walidatorow FluentValidation:
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // dodanie walidacji podczas obslugi komend przez MediatR:  
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
