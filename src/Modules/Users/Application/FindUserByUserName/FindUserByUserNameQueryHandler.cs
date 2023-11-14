namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    using LanguageExt;
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Modules.Users.Domain.Repositories;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Application.Queries;

    public class FindUserByUserNameQueryHandler :
        IQueryHandler<FindUserByUserNameQuery, Either<Exception, User>>
    {
        private readonly IUserReadRepository _userRepository;

        public FindUserByUserNameQueryHandler(IUserReadRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Either<Exception, User>> Handle(
            FindUserByUserNameQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(UserName.Create(request.UserName));

            if (user == null)
            {
                return new UserNotFoundByEmailError();
            }

            return user;
        }
    }
}