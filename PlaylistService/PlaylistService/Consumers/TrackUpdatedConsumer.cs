using Common;
using Contracts;
using MassTransit;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackUpdatedConsumer : IConsumer<TrackUpdated>
    {
        private readonly IRepository<Track> repository;

        public TrackUpdatedConsumer(IRepository<Track> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<TrackUpdated> context)
        {
            var message = context.Message;

            var track = await repository.GetAsync(message.Id);

            if (track == null)
            {
                track = new Track
                {
                    Id = message.Id,
                    Description = message.Description,
                    Duration = message.Duration,
                    Title = message.Title,
                    UrlId = message.UrlId,
                    ArtworkUrl = message.ArtworkUrl
                };

                await repository.CreateAsync(track);
            }
            else
            {
                //TODO: update only some properties?
                track.Description = message.Description;
                track.Duration = message.Duration;
                track.Title = message.Title;
                track.UrlId = message.UrlId;
                track.ArtworkUrl = message.ArtworkUrl;

                await repository.UpdateAsync(track);
            }

        }
    }
}
