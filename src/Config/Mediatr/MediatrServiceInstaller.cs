namespace OpenSkinsApi.Config.Mediatr
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using OpenSkinsApi.Infrastructure.Idempotence;

    public class MediatrServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                });

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        }
    }
}