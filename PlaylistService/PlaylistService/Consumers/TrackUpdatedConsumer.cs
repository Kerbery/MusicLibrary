using Common;
using Contracts;
using MassTransit;
using PlaylistService.Data;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackUpdatedConsumer : IConsumer<TrackUpdated>
    {
        //private readonly IRepository<Track> repository;

        private readonly PlaylistContext dbContext;

        public TrackUpdatedConsumer(/*IRepository<Track> repository,*/ PlaylistContext dbContext)
        {
            //this.repository = repository;
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TrackUpdated> context)
        {
            var message = context.Message;

            //var track = await repository.GetAsync(message.Id);
            var track = await dbContext.Tracks.FindAsync(message.Id);

            if (track == null)
            {
                track = new Track
                {
                    Id = message.Id,
                    Description = message.Description,
                    Duration = message.Duration,
                    Title = message.Title,
                    Permalink = message.Permalink,
                    ArtworkUrl = message.ArtworkUrl,
                    UserId = message.UserId,
                    UploadDate = DateTimeOffset.UtcNow,

                };

                //await repository.CreateAsync(track);
                dbContext.Tracks.Add(track);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                //TODO: update only some properties?
                track.Description = message.Description;
                track.Duration = message.Duration;
                track.Title = message.Title;
                track.Permalink = message.Permalink;
                track.ArtworkUrl = message.ArtworkUrl;
                track.UserId = message.UserId;

                //await repository.UpdateAsync(track);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
