using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrackService.Models;
using System;
using System.Linq;
using TrackService.DTOs.TrackDTOs;
using TrackService.Extensions;
using System.Threading.Tasks;
using TrackService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private static readonly List<Track> _tracks = new()
            {
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Title = "Automatic",
                    Description = "DeepHouse track by...",
                    Duration = TimeSpan.FromMinutes(3),
                    UploadDate = DateTimeOffset.UtcNow,
                    ArtworkUrl = "automatic.jpg",
                    MediaUrl = "automatic.mp3",
                    UrlId = "automatic"
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Title = "Let the sunshine",
                    Description = "DeepHouse track by...",
                    Duration = TimeSpan.FromMinutes(4),
                    UploadDate = DateTimeOffset.UtcNow,
                    ArtworkUrl = "let_the_sunhine.jpg",
                    MediaUrl = "let_the_sunhine.mp3",
                    UrlId = "let_the_sunhine"
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Title = "La tarde se ha puesto triste",
                    Description = "DeepHouse track by...",
                    Duration = TimeSpan.FromMinutes(5),
                    UploadDate = DateTimeOffset.UtcNow,
                    ArtworkUrl = "la_tarde_se_ha_puesto_triste.jpg",
                    MediaUrl = "la_tarde_se_ha_puesto_triste.mp3",
                    UrlId = "la_tarde_se_ha_puesto_triste"
                },
            };

        private readonly TrackRepository _trackRepository = new();

        // GET: api/<TracksController>
        [HttpGet]
        public async Task<IEnumerable<GetTrackDTO>> GetAsync()
        {
            var tracks = (await _trackRepository.GetAllAsync()).Select(track => track.AsDTO());
            return tracks;
        }

        // GET api/<TracksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrackDTO>> GetByIdAsync(Guid id)
        {
            var track = await _trackRepository.GetAsync(id);

            if (track == null) {
                return NotFound();
            }

            return track.AsDTO();
        }

        // POST api/<TracksController>
        [HttpPost]
        public async Task<ActionResult<GetTrackDTO>> PostAsync([FromBody] CreateTrackDTO trackDTO)
        {
            string title = trackDTO.Title.Replace(" ", "_").ToLowerInvariant();
            var track = new Track { 
                Id = Guid.NewGuid(),
                Duration = TimeSpan.FromTicks(trackDTO.Duration),
                Description = trackDTO.Description,
                Title = trackDTO.Title,
                UrlId = title,
                ArtworkUrl = $"{title}.jpeg",
                MediaUrl = $"{title}.mp3",
                UploadDate = DateTimeOffset.UtcNow,
            };

            await _trackRepository.CreateAsync(track);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = track.Id }, track.AsDTO());
        }

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateTrackDTO trackDTO)
        {
            var existingTrack = await _trackRepository.GetAsync(id);

            if (existingTrack == null)
            {
                return NotFound();
            }

            existingTrack.Id = existingTrack.Id;
            existingTrack.UploadDate = existingTrack.UploadDate;
            existingTrack.Title = trackDTO.Title;
            existingTrack.Description = trackDTO.Description;
            existingTrack.Duration = TimeSpan.FromTicks(trackDTO.Duration);
            existingTrack.MediaUrl = trackDTO.MediaUrl;
            existingTrack.ArtworkUrl = trackDTO.ArtworkUrl;
            existingTrack.UrlId = trackDTO.UrlId;

            await _trackRepository.UpdateAsync(existingTrack);
            
            return NoContent();
        }

        // DELETE api/<TracksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var track = await _trackRepository.GetAsync(id);

            if (track == null)
            {
                return NotFound();
            }

            await _trackRepository.RemoveAsync(track.Id);

            return NoContent();
        }
    }
}
