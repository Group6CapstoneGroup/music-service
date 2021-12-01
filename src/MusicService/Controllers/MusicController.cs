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
using MusicService.ControllerModels;
using Microsoft.AspNetCore.Cors;

//music-service requirment 1.1.0 Music service will contain a controller for music model. This controller is what the UI will ping when it make's request for information from music serivce.
namespace MusicService.Controllers
{
    //music-service requirment 1.1.1 The controller will contain a number of requests for GET, POST and DELETE functionality.
    [ApiController]
    [Route("/api/Music")]
    public class MusicController : Controller
    {
        private readonly IMusicService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Models.Music> _localizer;

        public MusicController(IMusicService service, IMapper mapper, IStringLocalizer<Models.Music> localizer)
        {
            _localizer = (IStringLocalizer<Models.Music>)(localizer ?? throw new ArgumentNullException(nameof(localizer)));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //music-service requirment 1.1.2 The first method in music controller will be GetMusic(). This method will return a list of music it gets back from music service.
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Models.Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<List<Models.Music>> GetMusic()
        {
            System.Diagnostics.Debug.WriteLine("Entering get all music tracks");

            //making call to song service and awaiting response
            var result = _service.GetAsync();

            if (result is null)
            {
                return null;
            }

            var musicList = new List<Models.Music>();
            await foreach (var music in result)
            {
               musicList.Add(_mapper.Map<Models.Music>(music));
            }

            return musicList;
        }

        //music-service requirment 1.1.3 The second method in music controller will be GetTrack(). This method will return a single music item with the corresponding record number the user entered in.
        [HttpGet("{recordNumber}")]
        [ProducesResponseType(typeof(IEnumerable<Models.Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTrack(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine("Entering get single track method");

            //making call to song service and awaiting response
            var result = await _service.GetAsync(recordNumber);

            if (result is null)
            {
                return NotFound(ServiceResponse.Error(_localizer["GetTrack404ErrorResponse"], StatusCodes.Status404NotFound));
            }
            return Ok(ServiceResponse.Successful(result));
        }

        //music-service requirment 1.1.4 The third method in music controller will be CreateMusicRecordAsync(). This method will create a new music model entry in the database with the fields: track, artist, album, and playlist specified by the user in the request.
        [HttpPost("")]
        [ProducesResponseType(typeof(Models.Music), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMusicRecordAsync(MusicCreate create)
        {
            System.Diagnostics.Debug.WriteLine("Entering Create");

            try
            {
                //making call to song service and awaiting response
                var track = await _service.CreateAsync(create);
                var created = _mapper.Map<Models.Music>(track);

                System.Diagnostics.Debug.WriteLine($"Leaving Create- {create.Track},{create.Artist},{create.Album},{create.Playlist}");

                return Created(Request.GetDisplayUrl() + $"/{created.RecordId}", ServiceResponse.Successful(created));
            }
            catch (ServiceException se)
            {
                return BadRequest(ServiceResponse.Error(se.Message, StatusCodes.Status400BadRequest));
            }
        }

        //music-service requirment 1.1.5 The fourth method in music controller will be DeleteTrack(). This method will delete the music object that corresponds to the record number entered in by the user.
        [HttpDelete("{recordNumber}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrack(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine("Entering Delete");

            //making call to song service and awaiting response
            var removed = await _service.DeleteAsync(recordNumber);
            System.Diagnostics.Debug.WriteLine($"Exiting Delete");
            if (removed)
            {
                return NoContent();
            }
            else
            {
                return NotFound(ServiceResponse.Error(_localizer["Music404ErrorResponse"], StatusCodes.Status404NotFound)); // TODO
            }
        }

    }
}
