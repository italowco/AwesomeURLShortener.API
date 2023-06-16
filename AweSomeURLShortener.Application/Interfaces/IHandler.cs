namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}