using Common;

namespace PlaylistService.Models
{
    public class Track : IEntity
    {
        public Guid Id { get; set; }
        public string UrlId { get; internal set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public double DurationSeconds => Duration.TotalSeconds;
        public string Description { get; set; }
        public string ArtworkUrl { get; set; }
        public DateTimeOffset UploadDate { get; set; }
    }
}
