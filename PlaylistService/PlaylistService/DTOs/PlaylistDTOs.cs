using PlaylistService.DTOs.PlaylistItemDTOs;
using System.ComponentModel.DataAnnotations;

namespace PlaylistService.DTOs.PlaylistDTOs
{
    public record GetPlaylistDTO(
        Guid PlaylistId,
        string Title,
        string Description,
        DateTimeOffset CreatedDate,
        IEnumerable<GetPlaylistItemDTO> Items);
    public record CreatePLaylistDTO([Required] string Title, string Description);
    public record UpdatePlaylistDTO(Guid PLaylistId, [Required] string Title, string Description);
    public record RemovePlaylistDTO(Guid PlaylistId);
}
