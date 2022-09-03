using Contracts;
using MassTransit;
using PlaylistService.Data;

namespace PlaylistService.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly PlaylistContext dbContext;

        public UserDeletedConsumer(PlaylistContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            var message = context.Message;

            var user = await dbContext.Users.FindAsync(message.Id);

            if (user == null)
            {
                return;
            }
            else
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
