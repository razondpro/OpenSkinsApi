namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    using OpenSkinsApi.Modules.Auth.Domain.Repositories;
    using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;
    using OpenSkinsApi.Modules.Auth.Application.Abstractions;
    using OpenSkinsApi.Application.Core;
    using LanguageExt;

    public class LoginUserUseCase : IUseCase<LoginUserRequestDto, LoginUserUseCaseResult>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IAuthUserReadRepository _userRepository;
        private readonly IAuthUnitOfWork _unitOfWork;

        public LoginUserUseCase(
            IAuthUserReadRepository userRepository,
            IJwtProvider jwtProvider,
            IAuthUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Exception, LoginUserUseCaseResult>> Execute(LoginUserRequestDto requestDto)
        {
            var email = Email.Create(requestDto.Email);

            var user = await _userRepository.Get(email);
            if (user is null)
            {
                return new UserNotFoundError();
            }

            if (!user.Verified)
            {
                return new UserNotVerifiedError();
            }

            // password could be null if the user was created with an external provider
            if (user.Password is null)
            {
                return new PasswordMismatchError();
            }

            var providedPassword = Password.Create(requestDto.Password, user.Password.Salt);
            if (!user.Password.Equals(providedPassword))
            {
                return new PasswordMismatchError();
            }

            var jwtToken = _jwtProvider.Generate(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.AddRefreshToken(refreshToken);

            await _unitOfWork.CommitAsync();

            return new LoginUserUseCaseResult(jwtToken, refreshToken);
        }
    }
}
