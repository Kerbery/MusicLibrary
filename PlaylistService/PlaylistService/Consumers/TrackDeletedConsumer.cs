using Common;
using Contracts;
using MassTransit;
using PlaylistService.Data;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackDeletedConsumer : IConsumer<TrackDeleted>
    {
        //private readonly IRepository<Track> repository;
        private readonly PlaylistContext dbContext;

        public TrackDeletedConsumer(/*IRepository<Track> repository,*/ PlaylistContext dbContext)
        {
            //this.repository = repository;
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TrackDeleted> context)
        {
            var message = context.Message;

            //var track = await repository.GetAsync(message.Id);
            var track = await dbContext.Tracks.FindAsync(message.Id);

            if (track == null)
            {
                return;
            }
            else
            {
                //await repository.RemoveAsync(message.Id);
                dbContext.Tracks.Remove(track);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
