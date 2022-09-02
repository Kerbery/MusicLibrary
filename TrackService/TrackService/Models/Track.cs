using Common;
using System;

namespace TrackService.Models
{
    public class Track : IEntity
    {
        public Guid Id { get; set; }
        public string Permalink { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public double DurationSeconds => Duration.TotalSeconds;
        public string Description { get; set; }
        public string ArtworkUrl { get; set; }
        public string MediaUrl { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public Guid UserId { get; set; }
    }
}
