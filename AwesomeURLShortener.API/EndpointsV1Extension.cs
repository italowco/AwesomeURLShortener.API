using AweSomeURLShortener.API.Validators;
using AweSomeURLShortener.Application.Commands;
using AweSomeURLShortener.Application.DTO;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace AweSomeURLShortener.API
{
    public static class EndpointsV1Extension
    {
        public static void AddEndpointsV1Extension(this WebApplication app)
        {
            app.MapPost("/registry", async ([FromBody] AddUrlDTO urlDTO, [FromServices] AddUrlValidator validator, [FromServices] IBusMediator busMediator)  => {
                // Call Commands
                var validationResult = validator.Validate(urlDTO);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await busMediator.Send(new AddUrlCommand(urlDTO));

                if (result.Succeded)
                    return Results.Ok(result);

                return Results.NoContent();

            });

            app.MapGet("/registry/{urlCode}", async ([FromRoute] string urlCode, [FromServices] IBusMediator busMediator) => {

                var result = await busMediator.Send(new GetUrlQuerie(urlCode));

                if (result.Succeded)
                    return Results.Ok(result);

                return Results.NotFound();
            });

            app.MapGet("/registry/", async ([FromQuery] string queryParams, [FromServices] IBusMediator busMediator) =>
            {
                var result = await busMediator.Send(new GetAllQuery(queryParams));

                if (result.Succeded)
                    return Results.Ok(result);

                return Results.NoContent();
            });

        }
    }
}
