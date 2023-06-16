using MassTransit;
using Microsoft.Extensions.Configuration;

namespace AweSomeURLShortener.Infrastructure
{
    internal class MassTransitConfigurationOptions
    {
        public string Type { get; set;  }

        public void Setup(IBusRegistrationConfigurator configurator, IConfigurationSection configuration)
        {

            const string IN_MEMORY = "InMemory";


            switch (Type)
            {
                case IN_MEMORY:
                    configurator.UsingInMemory((context, memory) => SetupBus(context, memory));
                    break;

                default:
                    throw new InvalidOperationException("Configure the message broker");
            }
        }

        private void SetupBus<T>(IBusRegistrationContext context, IReceiveConfigurator<T> configurator) where T : IReceiveEndpointConfigurator
        {
            configurator.ConfigureEndpoints(context);
        }
    } 
}
