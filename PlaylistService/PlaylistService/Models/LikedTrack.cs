using Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Models
{
    public class LikedTrack : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TrackId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        [ForeignKey(nameof(TrackId))]
        public Track Track { get; set; }
    }
}
