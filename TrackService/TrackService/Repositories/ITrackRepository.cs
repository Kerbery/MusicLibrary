using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Models;

namespace TrackService.Repositories
{
    public interface ITrackRepository
    {
        Task CreateAsync(Track entity);
        Task<IReadOnlyCollection<Track>> GetAllAsync();
        Task<Track> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Track entity);
    }
}