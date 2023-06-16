namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IResultError
    {
        string Error { get; }

        string Code { get; }
    }
}