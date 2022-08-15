using Common;

namespace PlaylistService.Models
{
    public class Playlist : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string? Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public Guid UserId { get; set; }
    }
}
