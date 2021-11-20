using MusicService.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    public interface ISelectedSongRepository
    {
        IAsyncEnumerable<Models.SelectedSong> GetAsync();
        Task<Models.SelectedSong> CreateAsync(SelectedSongCreate entry);
    }
}
