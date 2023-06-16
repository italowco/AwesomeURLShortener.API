using AweSomeURLShortener.Application.Interfaces;
using MassTransit;

namespace AweSomeURLShortener.Application
{
    public abstract class BaseMessageHandler<TRequest, TResponse> : IHandler<TRequest, TResponse>, IConsumer<TRequest>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
    {
        protected ConsumeContext<TRequest> CurrentContext { get; set; }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            CurrentContext = context;

            var result = await HandleAsync(context.Message);
            await context.RespondAsync(result);
        }

        public abstract Task<TResponse> HandleAsync(TRequest request);

    }
}
