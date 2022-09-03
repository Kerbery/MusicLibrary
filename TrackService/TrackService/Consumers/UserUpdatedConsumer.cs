using Common;
using Contracts;
using MassTransit;
using System.Threading.Tasks;
using TrackService.Models;

namespace TrackService.Consumers
{
    public class UserUpdatedConsumer : IConsumer<UserUpdated>
    {

        private readonly IRepository<User> repository;

        public UserUpdatedConsumer(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserUpdated> context)
        {
            var message = context.Message;

            var user = await repository.GetAsync(message.Id);

            if (user == null)
            {
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
            else
            {
                //TODO: update only some properties?
                user.FirstName = message.FirstName;
                user.LastName = message.LastName;
                user.AvatarUrl = message.AvatarUrl;
                user.Permalink = message.Permalink;
                user.UserName = message.UserName;

                await repository.UpdateAsync(user);
            }
        }
    }
}
