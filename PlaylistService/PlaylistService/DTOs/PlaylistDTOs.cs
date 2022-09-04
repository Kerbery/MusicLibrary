using PlaylistService.DTOs.PlaylistItemDTOs;
using System.ComponentModel.DataAnnotations;

namespace PlaylistService.DTOs.PlaylistDTOs
{
    public record GetPlaylistDTO(
        Guid PlaylistId,
        string Kind,
        string Title,
        string Description,
        DateTimeOffset CreatedDate,
        GetUserDTO User,
        IEnumerable<GetPlaylistItemDTO> Items);
    public record CreatePlaylistDTO([Required] string Title, string Description);
    public record UpdatePlaylistDTO(Guid PLaylistId, [Required] string Title, string Description);
    public record RemovePlaylistDTO(Guid PlaylistId);
}
