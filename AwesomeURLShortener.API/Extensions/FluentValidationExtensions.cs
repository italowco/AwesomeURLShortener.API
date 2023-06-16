using AweSomeURLShortener.API.Validators;
using AweSomeURLShortener.Application.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AweSomeURLShortener.API.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            
            services.AddScoped<AddUrlValidator>();

        }
    }
}
