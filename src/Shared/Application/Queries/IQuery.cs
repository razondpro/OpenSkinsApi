namespace OpenSkinsApi.Application.Queries
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}