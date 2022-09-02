using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Models
{
    public class Playlist : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public PlaylistKind Kind { get; set; }
        public string Title { get; set; } = String.Empty;
        public string? Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public ICollection<PlaylistItem> Items { get; set; }
    }
}
