using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.Models;

namespace PlaylistService.Business
{
    public class PlaylistItemsLogic : IPlaylistItemsLogic
    {
        private readonly PlaylistContext _context;

        public PlaylistItemsLogic(PlaylistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetPlaylistItemDTO>> GetPlaylistItems(Guid playlistId)
        {
            var playlistItems = await _context.PlaylistItems
                .Where(pi => pi.PlaylistId == playlistId)
                .Include(pi => pi.Track)
                .Select(pi => pi.AsDTO())
                .ToListAsync();

            return playlistItems;
        }

        public async Task<GetPlaylistItemDTO> GetPlaylistItem(Guid id)
        {
            var playlistItem = await _context.PlaylistItems
                .Include(pi => pi.Track)
                .SingleOrDefaultAsync(pi => pi.Id == id);

            return playlistItem.AsDTO();
        }

        public async Task<GetPlaylistItemDTO> CreatePlaylistItem(CreatePlaylistItemDTO createPlaylistItemDTO)
        {
            PlaylistItem playlistItem = new()
            {
                Id = Guid.NewGuid(),
                TrackId = createPlaylistItemDTO.TrackId,
                PlaylistId = createPlaylistItemDTO.PlaylistId,
                Position = 0,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            _context.PlaylistItems.Add(playlistItem);
            await _context.SaveChangesAsync();

            var savedPlaylistItem = await _context.PlaylistItems
                .Include(pi => pi.Track)
                .ThenInclude(t => t.User)
                .SingleOrDefaultAsync(pi => pi.Id == playlistItem.Id);

            return savedPlaylistItem.AsDTO();
        }

        public async Task<int> UpdatePlaylistItem(Guid id, UpdatePlaylistItemDTO updatePlaylistItemDTO)
        {
            var existingPlaylistItem = await _context.PlaylistItems
                .Include(pi => pi.Track)
                .SingleOrDefaultAsync(pi => pi.Id == id); ;

            if (existingPlaylistItem == null)
            {
                throw new KeyNotFoundException("PlaylistItem not found");
            }

            existingPlaylistItem.Position = updatePlaylistItemDTO.Position;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePlaylistItem(Guid id)
        {
            var existingPlaylistItem = await _context.PlaylistItems.FindAsync(id);

            if (existingPlaylistItem == null)
            {
                throw new KeyNotFoundException("PlaylistItem not found");
            }

            _context.PlaylistItems.Remove(existingPlaylistItem);

            return await _context.SaveChangesAsync();
        }
    }
}
