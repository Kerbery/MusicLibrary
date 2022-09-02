using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Models
{
    public class PlaylistItem : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        public Playlist Playlist { get; set; }
        public uint Position { get; set; }
        public Guid TrackId { get; set; }
        [ForeignKey(nameof(TrackId))]
        public Track? Track { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
