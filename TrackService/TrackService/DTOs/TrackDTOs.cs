using System;
using System.ComponentModel.DataAnnotations;

namespace TrackService.DTOs.TrackDTOs
{
    public record CreateTrackDTO([Required] string Title, string Description, [Range(0, long.MaxValue)] long Duration);
    public record GetTrackDTO(Guid Id, string Title, string Description, double Duration, string MediaUrl, string ArtworkUrl, string Permalink, DateTimeOffset UploadDate, Guid UserId);
    public record UpdateTrackDTO([Required] string Title, string Description, [Range(0, long.MaxValue)] long Duration, [Required] string MediaUrl, [Required] string ArtworkUrl, [Required] string UrlId);
    public record DeleteTrackDTO(Guid Id);
}
