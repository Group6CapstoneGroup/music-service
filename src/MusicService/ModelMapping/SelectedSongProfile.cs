using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.ModelMapping
{
    public class SelectedSongProfile : Profile
    {
        public SelectedSongProfile()
        {
            CreateMap<Models.SelectedSong, ControllerModels.SelectedSongCreate>().ReverseMap();

        }
    }
}
