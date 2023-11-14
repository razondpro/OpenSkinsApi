using KafkaFlow;
using KafkaFlow.TypedHandler;
using MediatR;
using OpenSkinsApi.Infrastructure.Bus.Kafka.Events.Schemas.Auth.UserCreated;
using OpenSkinsApi.Modules.Users.Application.CreateUser;
using Serilog;

namespace OpenSkinsApi.Modules.Users.Infrastructure.Bus.Kafka.Consumer.Handlers
{
    public class UserCreatedConsumerEventHandler : IMessageHandler<UserCreated>
    {
        private readonly IMediator mediator;

        public UserCreatedConsumerEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Handle(IMessageContext context, UserCreated message)
        {
            Log.Information("UserCreatedConsumerEventHandler: {message}", message);
            try
            {
                var cmd = new CreateUserCommand(
                    message.UserId,
                    message.UserName,
                    message.FirstName,
                    message.LastName
                );

                var result = await mediator.Send(cmd, context.ConsumerContext.WorkerStopped);

                if (result.IsLeft)
                {
                    Log.Error("Error in UserCreatedConsumerEventHandler: {error}", result);
                }
                else
                {
                    context.ConsumerContext.StoreOffset();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in UserCreatedConsumerEventHandler");
            }
        }
    }
}