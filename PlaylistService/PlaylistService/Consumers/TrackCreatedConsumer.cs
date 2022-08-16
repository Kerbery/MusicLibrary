using Common;
using Contracts;
using MassTransit;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackCreatedConsumer : IConsumer<TrackCreated>
    {
        private readonly IRepository<Track> repository;

        public TrackCreatedConsumer(IRepository<Track> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<TrackCreated> context)
        {
            var message = context.Message;

            var track = await repository.GetAsync(message.Id);

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
                UrlId = message.UrlId,
                ArtworkUrl = message.ArtworkUrl
            };

            await repository.CreateAsync(track);
        }
    }
}
