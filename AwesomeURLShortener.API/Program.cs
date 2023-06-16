using AweSomeURLShortener.API;
using AweSomeURLShortener.API.Extensions;
using AweSomeURLShortener.Application;
using AweSomeURLShortener.Infrastructure;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
builder.Services.AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Add Layers
builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddApplicationLayer();


var app = builder.Build();

//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AwesomeURLShortenerDbContext>();
    db.Database.Migrate();

    DataSeeder.Seed(db, "./urlBase.json");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
app.AddEndpointsV1Extension();

app.Run();
