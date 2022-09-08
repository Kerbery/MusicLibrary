using System;
using System.ComponentModel.DataAnnotations;
using TrackService.Models;

namespace TrackService.DTOs.TrackDTOs
{
    public record CreateTrackDTO(Guid Id,[Required] string Title, string Description, [Range(0, long.MaxValue)] long Duration, Guid UserId, string ArtworkUrl, string MediaUrl, string Permalink);
    public record GetTrackDTO(Guid Id, string Title, string Description, double Duration, string MediaUrl, string ArtworkUrl, string Permalink, DateTimeOffset UploadDate, Guid UserId, User User);
    public record UpdateTrackDTO([Required] string Title, string Description, [Range(0, long.MaxValue)] long Duration, [Required] string MediaUrl, [Required] string ArtworkUrl, [Required] string Permalink);
    public record DeleteTrackDTO(Guid Id);
}
