using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
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

        public async Task<IEnumerable<GetPlaylistDTO>> GetUserPlaylists(Guid userId)
        {
            var playlists = await _context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items)
                .ThenInclude(pi => pi.Track)
                .ThenInclude(t => t.User)
                .Select(pl => pl.AsDTO())
                .ToListAsync(); 

            return playlists;
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
