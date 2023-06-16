using AweSomeURLShortener.Application.Interfaces;
using MassTransit.Mediator;

namespace AweSomeURLShortener.Infrastructure
{
    public class MassTransitMediator : IBusMediator
    {
        private readonly IMediator _mediator;

        public MassTransitMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<TEvent>(TEvent eventMessage) where TEvent : class
        {
            await _mediator.Publish(eventMessage);
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var client = _mediator.CreateRequestClient<IRequest<TResponse>>();

            cancellationToken.ThrowIfCancellationRequested();
            
            var response = await client.GetResponse<TResponse>(request, cancellationToken);

            return response.Message;
        }
    }
}
