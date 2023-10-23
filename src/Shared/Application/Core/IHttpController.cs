namespace OpenSkinsApi.Application.Core
{
    public interface IHttpController<TRequest, TResult>
        where TResult : class, IResult
    {
        Task<TResult> Execute(TRequest request, CancellationToken cancellationToken = default);
    }
}