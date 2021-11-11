using MusicService.ControllerModels;
using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    public interface IMusicService
    {
        IAsyncEnumerable<Models.Music> GetAsync();
        Task<Models.Music> GetAsync(long recordNumber);

        Task<Models.Music> CreateAsync(MusicCreate entry);

        Task<bool> DeleteAsync(long recordNumber);
    }
}
