﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrackService.Models;
using System;
using TrackService.DTOs.TrackDTOs;
using System.Threading.Tasks;
using Common;
using MassTransit;
using Contracts;
using AutoMapper;
using System.Linq;

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
                    Permalink = "automatic"
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
                    Permalink = "let_the_sunhine"
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
                    Permalink = "la_tarde_se_ha_puesto_triste"
                },
            };

        private readonly IRepository<Track> _trackRepository;

        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public TracksController(IRepository<Track> trackRepository, IPublishEndpoint endpoint, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _publishEndpoint = endpoint;
            _mapper = mapper;
        }

        // GET: api/<TracksController>
        [HttpGet]
        public async Task<IEnumerable<GetTrackDTO>> GetAsync()
        {
            var tracks = (await _trackRepository.GetAllAsync()).Select(t=>_mapper.Map<GetTrackDTO>(t));//.Select(track => track.AsDTO());
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


            return _mapper.Map<GetTrackDTO>(track);
        }

        // GET api/<TracksController>/permalink/Id
        [HttpGet("permalink/{permalink}")]
        public async Task<ActionResult<GetTrackDTO>> GetByPermalinkAsync(string permalink)
        {
            if (string.IsNullOrEmpty(permalink))
            {
                return BadRequest();
            }

            var track = await _trackRepository.GetAsync(track => track.Permalink == permalink);

            if (track == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetTrackDTO>(track);
        }

        // POST api/<TracksController>
        [HttpPost]
        public async Task<ActionResult<GetTrackDTO>> PostAsync([FromBody] CreateTrackDTO trackDTO)
        {
            string title = trackDTO.Title.Replace(" ", "_").ToLowerInvariant();
            var track = new Track { 
                Id = Guid.NewGuid(),
                Duration = TimeSpan.FromSeconds(trackDTO.Duration),
                Description = trackDTO.Description,
                Title = trackDTO.Title,
                Permalink = title,
                ArtworkUrl = $"{title}.jpeg",
                MediaUrl = $"{title}.mp3",
                UploadDate = DateTimeOffset.UtcNow,
                UserId = Guid.Parse("F32821A4-8810-41F1-9846-542690624EFF"),
            };

            await _trackRepository.CreateAsync(track);

            await _publishEndpoint.Publish(_mapper.Map<TrackCreated>(track));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = track.Id }, _mapper.Map<GetTrackDTO>(track));
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
            existingTrack.Permalink = trackDTO.UrlId;

            await _trackRepository.UpdateAsync(existingTrack);

            await _publishEndpoint.Publish(_mapper.Map<TrackUpdated>(existingTrack));

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
            await _publishEndpoint.Publish(new TrackDeleted(track.Id));

            return NoContent();
        }
    }
}
