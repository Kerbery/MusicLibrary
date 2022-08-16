using Common;
using Contracts;
using MassTransit;
using PlaylistService.Models;

namespace PlaylistService.Consumers
{
    public class TrackDeletedConsumer : IConsumer<TrackDeleted>
    {
        private readonly IRepository<Track> repository;

        public TrackDeletedConsumer(IRepository<Track> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<TrackDeleted> context)
        {
            var message = context.Message;

            var track = await repository.GetAsync(message.Id);

            if (track == null)
            {
                return;
            }
            else
            {
                await repository.RemoveAsync(message.Id);
            }
        }
    }
}
