using Common;
using Contracts;
using MassTransit;
using System.Threading.Tasks;
using TrackService.Models;

namespace TrackService.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly IRepository<User> repository;

        public UserDeletedConsumer(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            var message = context.Message;

            var user = await repository.GetAsync(message.Id);

            if (user == null)
            {
                return;
            }
            else
            {
                await repository.RemoveAsync(message.Id);
            }
        }
    }
}
