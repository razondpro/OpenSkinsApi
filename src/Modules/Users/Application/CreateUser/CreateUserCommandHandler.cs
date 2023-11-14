namespace OpenSkinsApi.Modules.Users.Application.CreateUser
{
    using OpenSkinsApi.Modules.Users.Domain.Repositories;
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Application.Commands;
    using LanguageExt;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Users.Application.Abstractions;

    public class CreateUserCommandHandler :
        ICommandHandler<CreateUserCommand, Either<Exception, Unit>>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUserWriteRepository userRepository,
            IUserReadRepository userReadRepository,
            IUserUnitOfWork unitOfWork
            )
        {
            _userWriteRepository = userRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Exception, Unit>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var userName = UserName.Create(request.UserName);

            if (await _userReadRepository.Get(userName) is not null)
            {
                return new UserNameAlreadyExistsError();
            }

            var name = Name.Create(request.FirstName, request.LastName);
            var userId = new UniqueIdentity(Guid.Parse(request.UserId));


            var user = User.Create(userId, userName, name);

            await _userWriteRepository.Create(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Default;
        }
    }
}