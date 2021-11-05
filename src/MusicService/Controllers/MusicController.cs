using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MusicService.Models;
using MusicService.Common;
using MusicService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using MusicService.Common.Exceptions;

namespace MusicService.Controllers
{
    [ApiController]
    [Route("/api/Music")]
    public class MusicController : Controller
    {
        private readonly IMusicService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Music> _localizer;

        public MusicController(IMusicService service, IMapper mapper, IStringLocalizer<Music> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMusic()
        {
            System.Diagnostics.Debug.WriteLine("Entering get all music tracks");

            var result = _service.GetAsync();

            if (result is null)
            {
                return NoContent();
            }

            var musicList = new List<Music>();
            await foreach (var music in result)
            {
               musicList.Add(_mapper.Map<Music>(music));
            }

            return Ok(ServiceResponse.Successful(musicList));
        }

        [HttpGet("{trackName}")]
        [ProducesResponseType(typeof(IEnumerable<Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTrack(string trackName)
        {
            System.Diagnostics.Debug.WriteLine("Entering get single track method");

            var result = await _service.GetAsync(trackName);

            if (result is null)
            {
                return NotFound(ServiceResponse.Error(_localizer["GetTrack404ErrorResponse"], StatusCodes.Status404NotFound));
            }
            return Ok(ServiceResponse.Successful(result));
        }

        [HttpPost("{trackName}{artist}{album}{playlist}")]
        [ProducesResponseType(typeof(Music), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCostDivisionAsync(string trackName, string artist, string album, string playlist)
        {
            System.Diagnostics.Debug.WriteLine("Entering Create");

            try
            {
                var track = await _service.CreateAsync(trackName, artist, album, playlist);
                var created = _mapper.Map<Music>(track);

                System.Diagnostics.Debug.WriteLine($"Leaving Create- {trackName}{artist}{album}{playlist}");

                return Created(Request.GetDisplayUrl() + $"/{created.RecordId}", ServiceResponse.Successful(created));
            }
            catch (ServiceException se)
            {
                return BadRequest(ServiceResponse.Error(se.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpDelete("{trackName}{artist}{album}{playlist}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrack(string trackName, string artist, string album, string playlist)
        {
            System.Diagnostics.Debug.WriteLine("Entering Delete");

            var removed = await _service.DeleteAsync(trackName, artist, album, playlist);
            System.Diagnostics.Debug.WriteLine($"Exiting Delete");
            if (removed)
            {
                return NoContent();
            }
            else
            {
                return NotFound(ServiceResponse.Error(_localizer["CostDivision404ErrorResponse"], StatusCodes.Status404NotFound)); // TODO
            }
        }

    }
}
