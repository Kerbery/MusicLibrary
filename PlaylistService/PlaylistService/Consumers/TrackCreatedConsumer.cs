using Contracts;
using MassTransit;
using PlaylistService.Data;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackCreatedConsumer : IConsumer<TrackCreated>
    {
        //private readonly IRepository<Track> repository;

        private readonly PlaylistContext dbContext;

        public TrackCreatedConsumer(/*IRepository<Track> repository,*/ PlaylistContext context)
        {
            //this.repository = repository;
            this.dbContext = context;
        }

        public async Task Consume(ConsumeContext<TrackCreated> context)
        {
            var message = context.Message;

            //var track = await repository.GetAsync(message.Id);
            var track = await dbContext.Tracks.FindAsync(message.Id);


            if(track != null)
            {
                return;
            }

            track = new Track
            {
                Id = message.Id,
                Description = message.Description,
                Duration = message.Duration,
                UploadDate = message.UploadDate,
                Title = message.Title,
                Permalink = message.Permalink,
                ArtworkUrl = message.ArtworkUrl,
                UserId = message.UserId,

            };

            //await repository.CreateAsync(track);
            dbContext.Tracks.Add(track);
            await dbContext.SaveChangesAsync();
        }
    }
}
