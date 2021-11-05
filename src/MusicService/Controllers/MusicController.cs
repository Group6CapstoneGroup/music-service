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

namespace MusicService.Controllers
{
    [ApiController]
    [Route("/api/Music")]
    public class MusicController : Controller
    {
        private readonly IMusicService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Music> _localizer;

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCostDivisions()
        {
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

    }
}
