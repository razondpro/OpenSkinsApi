using LanguageExt;

namespace OpenSkinsApi.Application.Core
{
    public interface IUseCase<TRequestDto, TResult> where TResult : class
    {
        Task<Either<Exception, TResult>> Execute(TRequestDto requestDto);
    }
}