using Contracts;
using MassTransit;
using PlaylistService.Data;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class UserUpdatedConsumer : IConsumer<UserUpdated>
    {

        private readonly PlaylistContext dbContext;

        public UserUpdatedConsumer( PlaylistContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UserUpdated> context)
        {
            var message = context.Message;

            var user = await dbContext.Users.FindAsync(message.Id);

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

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                //TODO: update only some properties?
                user.FirstName = message.FirstName;
                user.LastName = message.LastName;
                user.AvatarUrl = message.AvatarUrl;
                user.Permalink = message.Permalink;
                user.UserName = message.UserName;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
