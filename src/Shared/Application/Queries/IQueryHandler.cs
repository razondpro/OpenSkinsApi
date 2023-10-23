namespace OpenSkinsApi.Application.Queries
{
    using MediatR;
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {

    }
}