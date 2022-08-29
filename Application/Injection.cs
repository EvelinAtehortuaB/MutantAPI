using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Injection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(assembly));
            services.AddMediatR(assembly);
            return services;
        }
    }
}
