using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MusicService.Common;
using MusicService.Common.Exceptions;
using MusicService.Models;
using MusicService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Controllers
{
    [ApiController]
    [Route("/api/SelectedSongs")]
    public class SelectedSongsController : Controller
    {
        private readonly ISelectedSongService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Models.SelectedSong> _localizer;

        public SelectedSongsController(ISelectedSongService service, IMapper mapper, IStringLocalizer<SelectedSong> localizer)
        {
            _localizer = (IStringLocalizer<Models.SelectedSong>)(localizer ?? throw new ArgumentNullException(nameof(localizer)));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Models.SelectedSong>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<List<Models.SelectedSong>> GetSelectedSongs()
        {
            System.Diagnostics.Debug.WriteLine("Entering get all music tracks");

            var result = _service.GetSelectedSongs();

            if (result is null)
            {
                return null;
            }

            var musicList = new List<Models.SelectedSong>();
            await foreach (var music in result)
            {
                musicList.Add(_mapper.Map<Models.SelectedSong>(music));
            }

            return musicList;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Models.SelectedSong), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSelectedSongRecord(ControllerModels.SelectedSongCreate create)
        {
            System.Diagnostics.Debug.WriteLine("Entering Create");

            try
            {
                var track = await _service.CreateSongRecord(create);
                var created = _mapper.Map<Models.SelectedSong>(track);

                System.Diagnostics.Debug.WriteLine($"Leaving Create- {create.Track},{create.Artist},{create.Album},{create.Playlist}");

                return Created(Request.GetDisplayUrl() + $"/{created.RecordId}", ServiceResponse.Successful(created));
            }
            catch (ServiceException se)
            {
                return BadRequest(ServiceResponse.Error(se.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpDelete("List")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSongSelection()
        {
            System.Diagnostics.Debug.WriteLine($"Entering Delete");

            var removed = await _service.DeleteAsync();
            System.Diagnostics.Debug.WriteLine($"Exiting Delete");
            if (removed)
            {
                return NoContent();
            }
            else
            {
                return NotFound(ServiceResponse.Error(_localizer["SelectedSongList404ErrorResponse"], StatusCodes.Status404NotFound)); // TODO
            }
        }
    }
}
