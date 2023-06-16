using AweSomeURLShortener.Application.Commands;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Application.Queries;
using AweSomeURLShortener.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AweSomeURLShortener.Infrastructure
{
    public static class ConfigureDependeciesExtension
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AwesomeURLShortenerDbContext>(options =>
            {
                var connectionString = configuration.GetRequiredSection("Database:ConnectionString").Value;
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBusMediator, MassTransitMediator>();
            services.AddScoped<IUrlRegistryRepository, UrlRegistryRepository>();
            


            services.AddMassTransit(options =>
            {
                EndpointConvention.Map<IQueueMessage>(new Uri("queue:cache-queue"));

                options.AddMediator(configure =>
                {
                    configure.AddConsumer<AddUrlCommandHandler>();
                    configure.AddConsumer<GetUrlQuerieHandler>();
                    configure.AddConsumer<GetAllQueryHandler>();
                });

                ConfigureMassTransit(options, configuration);
            });

        }

        private static void ConfigureMassTransit(IBusRegistrationConfigurator options, IConfiguration configuration)
        {
            IConfigurationSection messageBrokerSection = configuration.GetRequiredSection("MessageBroker");
            var massTranssitConfiguration = messageBrokerSection.Get<MassTransitConfigurationOptions>();

            massTranssitConfiguration.Setup(options, messageBrokerSection);

        }

        
    }
}
