namespace OpenSkinsApi.Config.Mediatr
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork.Behaviors;

    public class MediatrServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                    cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
                });
        }
    }
}