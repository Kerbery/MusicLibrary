using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.LikedTrackDTOs;
using PlaylistService.DTOs.Paging;
using PlaylistService.Models;

namespace PlaylistService.Business
{
    public class LikedTrackLogic : ILikedTrackLogic
    {
        private readonly PlaylistContext _context;

        public LikedTrackLogic(PlaylistContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetLikedTrackDTO>> GetLikedTracks(Guid userId, string userPermalink, PagingParameters pagingParameters)
        {
            if (!string.IsNullOrEmpty(userPermalink))
            {
                userId = (await _context.Users.SingleOrDefaultAsync(u => u.Permalink == userPermalink)).Id;
            }

            var querry = _context.LikedTracks
                .Include(lt => lt.Track)
                .ThenInclude(t => t.User)
                .Where(lt => lt.UserId == userId)
                .OrderByDescending(pi => pi.CreatedDate)
                .Select(lt => lt.AsDTO());


            return await PagedList<GetLikedTrackDTO>.ToPagedList(querry,
                pagingParameters.PageNumber,
                pagingParameters.PageSize);
        }

        public async Task<int> LikeTrack(Guid userId, Guid trackId)
        {
            var isTrackLiked = await _context.LikedTracks.AnyAsync(lt => lt.TrackId == trackId && lt.UserId == userId);
            if (isTrackLiked)
            {
                return 0;
            }
            else
            {
                LikedTrack likedTrack = new LikedTrack
                {
                    UserId = userId,
                    TrackId = trackId,
                    CreatedDate = DateTimeOffset.UtcNow
                };
                _context.LikedTracks.Add(likedTrack);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UnLikeTrack(Guid userId, Guid trackId)
        {
            var likedTrack = await _context.LikedTracks.SingleOrDefaultAsync(lt => lt.TrackId == trackId && lt.UserId == userId);
            if (likedTrack is null)
            {
                return 0;
            }
            else
            {
                _context.LikedTracks.Remove(likedTrack);
                return await _context.SaveChangesAsync();
            }
        }
    }
}
