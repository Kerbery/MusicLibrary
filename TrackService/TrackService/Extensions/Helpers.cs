using TrackService.DTOs.TrackDTOs;
using TrackService.Models;

namespace TrackService.Extensions
{
    public static class Helpers
    {
        public static GetTrackDTO AsDTO(this Track track, User user)
        {
            return new GetTrackDTO
                (
                    Id: track.Id,
                    ArtworkUrl: track.ArtworkUrl,
                    MediaUrl: track.MediaUrl,
                    Permalink: track.Permalink,
                    Title: track.Title,
                    Description: track.Description,
                    Duration: track.DurationSeconds,
                    UploadDate: track.UploadDate,
                    UserId: track.UserId,
                    User: user
                );
        }
    }
}
