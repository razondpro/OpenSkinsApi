namespace OpenSkinsApi.Modules.Auth.Application.RegisterUser
{
    using OpenSkinsApi.Modules.Auth.Domain.Repositories;
    using OpenSkinsApi.Modules.Auth.Domain.Entities;
    using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;
    using OpenSkinsApi.Application.Commands;
    using LanguageExt;
    using OpenSkinsApi.Modules.Auth.Application.Abstractions;

    public class RegisterUserCommandHandler :
        ICommandHandler<RegisterUserCommand, Either<Exception, Unit>>
    {
        private readonly IAuthUserWriteRepository _userWriteRepository;
        private readonly IAuthUserReadRepository _userReadRepository;
        private readonly IAuthUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IAuthUserWriteRepository userRepository,
            IAuthUserReadRepository userReadRepository,
            IAuthUnitOfWork unitOfWork
            )
        {
            _userWriteRepository = userRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Exception, Unit>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (await _userReadRepository.Get(email) is not null)
            {
                return new EmailAlreadyExistsError();
            }

            var userName = UserName.Create(request.UserName);
            if (await _userReadRepository.Get(userName) is not null)
            {
                return new UserNameAlreadyExistsError();
            }

            var name = Name.Create(request.FirstName, request.LastName);

            var user = User.Create(null, email, name, userName);

            var password = Password.Create(request.Password);
            user.SetPassword(password);

            await _userWriteRepository.Create(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Default;
        }
    }
}