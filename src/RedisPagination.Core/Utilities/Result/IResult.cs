namespace RedisPagination.Core
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
