using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Models
{
    public class Track : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Permalink { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public double DurationSeconds => Duration.TotalSeconds;
        public string Description { get; set; }
        public string ArtworkUrl { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
