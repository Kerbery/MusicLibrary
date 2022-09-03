using Contracts;
using MassTransit;
using PlaylistService.Data;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly PlaylistContext dbContext;

        public UserCreatedConsumer(PlaylistContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;

            var user = await dbContext.Users.FindAsync(message.Id);


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

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
