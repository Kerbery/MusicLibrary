using TrackService.DTOs.TrackDTOs;
using TrackService.Models;

namespace TrackService.Extensions
{
    public static class Helpers
    {
        public static GetTrackDTO AsDTO(this Track track)
        {
            return new GetTrackDTO
                (
                    Id: track.Id,
                    ArtworkUrl: track.ArtworkUrl,
                    MediaUrl: track.MediaUrl,
                    UrlId: track.UrlId,
                    Title: track.Title,
                    Description: track.Description,
                    Duration: track.DurationTicks,
                    UploadDate: track.UploadDate
                );
        }
    }
}
