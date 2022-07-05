using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrackService.Models;
using System;
using System.Linq;
using TrackService.DTOs.TrackDTOs;
using TrackService.Extensions;

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

        // GET: api/<TracksController>
        [HttpGet]
        public ActionResult<IEnumerable<GetTrackDTO>> Get()
        {
            return _tracks
                .Select(track => track.AsDTO())
                .ToList();
        }

        // GET api/<TracksController>/5
        [HttpGet("{id}")]
        public ActionResult<GetTrackDTO> GetById(Guid id)
        {
            var track = _tracks
                .Where(track => track.Id == id)
                .SingleOrDefault();

            if (track == null) {
                return NotFound();
            }

            return track.AsDTO();
        }

        // POST api/<TracksController>
        [HttpPost]
        public ActionResult<GetTrackDTO> Post([FromBody] CreateTrackDTO trackDTO)
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

            _tracks.Add(track);

            return CreatedAtAction(nameof(GetById), new { id = track.Id }, track.AsDTO());
        }

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateTrackDTO trackDTO)
        {
            var existingTrack = _tracks.Where(track => track.Id == id).SingleOrDefault();

            if (existingTrack == null)
            {
                return NotFound();
            }

            var updatedTrack = new Track
            {
                Id = existingTrack.Id,
                UploadDate = existingTrack.UploadDate,
                Title = trackDTO.Title,
                Description = trackDTO.Description,
                Duration = TimeSpan.FromTicks(trackDTO.Duration),
                MediaUrl = trackDTO.MediaUrl,
                ArtworkUrl = trackDTO.ArtworkUrl,
                UrlId = trackDTO.UrlId
            };

            var index = _tracks.FindIndex(existingTrack => existingTrack.Id == id);
            _tracks[index] = updatedTrack;
            return NoContent();
        }

        // DELETE api/<TracksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = _tracks.FindIndex(existingTrack => existingTrack.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            _tracks.RemoveAt(index);
            return NoContent();
        }
    }
}
