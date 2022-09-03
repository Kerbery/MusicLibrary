using Common;
using Contracts;
using MassTransit;
using System.Threading.Tasks;
using TrackService.Models;

namespace TrackService.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IRepository<User> repository;

        public UserCreatedConsumer( IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;

            var user = await repository.GetAsync(message.Id);


            if (user != null)
            {
                return;
            }

            user = new User
            {
                Id = message.Id,
                UserName = message.UserName,
                FirstName = message.FirstName,
                LastName = message.LastName,
                AvatarUrl = message.AvatarUrl,
                Permalink = message.Permalink,
            };

            await repository.CreateAsync(user);
        }
    }
}
