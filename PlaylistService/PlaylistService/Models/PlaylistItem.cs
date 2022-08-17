using Common;

namespace PlaylistService.Models
{
    public class PlaylistItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public uint Position { get; set; }
        public Guid TrackId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
