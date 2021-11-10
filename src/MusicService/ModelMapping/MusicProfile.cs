using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.ModelMapping
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<Models.Music, ControllerModels.Music>().ReverseMap();
        }  
    }
}
