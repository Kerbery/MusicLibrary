namespace PlaylistService.DTOs.TrackDTOs
{
    public record GetTrackDTO(
        Guid Id, 
        string Title, 
        string Description, 
        double Duration, 
        string ArtworkUrl, 
        string Permalink, 
        DateTimeOffset UploadDate, 
        GetUserDTO User);
}
