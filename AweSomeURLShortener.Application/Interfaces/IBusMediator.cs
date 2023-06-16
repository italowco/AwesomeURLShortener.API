namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IBusMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class;

        Task Publish<TEvent>(TEvent eventMessage) where TEvent : class;
    }
}
