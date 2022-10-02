using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.Paging;
using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.Models;

namespace PlaylistService.Business
{
    public class PlaylistLogic : IPlaylistLogic
    {
        private readonly PlaylistContext _context;

        public PlaylistLogic(PlaylistContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetPlaylistDTO>> GetUserPlaylists(Guid userId, string userPermalink, PagingParameters pagingParameters)
        {
            if (!string.IsNullOrEmpty(userPermalink))
            {
                userId = (await _context.Users.SingleOrDefaultAsync(u => u.Permalink == userPermalink)).Id;
            }

            var querry = _context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items.OrderByDescending(i=>i.CreatedDate))
                .ThenInclude(pi => pi.Track)
                .ThenInclude(t => t.User)
                .Where(u => u.UserId == userId)
                .Select(pl => pl.AsDTO());

            return await PagedList<GetPlaylistDTO>.ToPagedList(querry,
                pagingParameters.PageNumber,
                pagingParameters.PageSize);
        }

        public async Task<GetPlaylistDTO> GetPlaylist(Guid playlistId)
        {
            var playlist = await _context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items)
                .ThenInclude(pi => pi.Track)
                .ThenInclude(t => t.User)
                .Where(pl => pl.Id == playlistId)
                .Select(pl => pl.AsDTO())
                .SingleOrDefaultAsync();

            return playlist;
        }

        public async Task<GetPlaylistDTO> CreatePlaylist(CreatePlaylistDTO createPlaylistDTO)
        {
            Playlist playlist = new()
            {
                Id = Guid.NewGuid(),
                Title = createPlaylistDTO.Title,
                Description = createPlaylistDTO.Description,
                CreatedDate = DateTimeOffset.UtcNow,
                UserId = Guid.Parse("e6f8d7d7-76d2-4e8e-8371-ba8d732e94e9"),
                Kind = PlaylistKind.Likes
            };

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();


            var savedPlaylist = await _context.Playlists
                .Include(pl => pl.User)
                .SingleOrDefaultAsync(pl => pl.Id == playlist.Id);
            return savedPlaylist.AsDTO();
        }

        public async Task<int> UpdatePlaylist(Guid playlistId, UpdatePlaylistDTO updatePlaylistDTO)
        {
            var existingPlaylist = await _context.Playlists.FindAsync(playlistId);

            if (existingPlaylist == null)
            {
                throw new KeyNotFoundException("Playlist not found");
            }

            existingPlaylist.Title = updatePlaylistDTO.Title;
            existingPlaylist.Description = updatePlaylistDTO.Description;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePlaylist(Guid playlistId)
        {
            var existingPlaylist = await _context.Playlists.FindAsync(playlistId);

            if (existingPlaylist == null)
            {
                throw new KeyNotFoundException("Playlist not found");
            }

            _context.Playlists.Remove(existingPlaylist);
            return await _context.SaveChangesAsync();
        }
    }
}
