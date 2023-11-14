namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using LanguageExt;
    using OpenSkinsApi.Modules.Users.Domain.Repositories;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Application.Commands;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Users.Application.Abstractions;

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Either<Exception, Unit>>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IUserUnitOfWork unitOfWork
        )
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Either<Exception, Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var userId = new UniqueIdentity(Guid.Parse(request.UserId));

            var user = await _userReadRepository.Get(userId);

            if (user is null)
            {
                return new UserNotFoundError();
            }

            UpdateUser(user, request);

            await _userWriteRepository.Update(user);
            await _unitOfWork.CommitAsync();

            return Unit.Default;
        }


        private static void UpdateUser(User user, UpdateUserCommand request)
        {
            if (request.FirstName is not null)
            {
                var name = Name.Create(request.FirstName, user.Name.LastName);
                user.UpdateName(name);
            }

            if (request.LastName is not null)
            {
                var name = Name.Create(user.Name.FirstName, request.LastName);
                user.UpdateName(name);
            }
        }
    }
}