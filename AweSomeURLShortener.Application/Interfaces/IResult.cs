namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IResult
    {
        IReadOnlyCollection<IResultError> Errors { get; }

        string Message { get; }

        bool Succeded { get;  }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}