using MusicService.ControllerModels;
using MusicService.Models;
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
        Task<bool> DeleteAsync(IAsyncEnumerable<SelectedSong> keys);
    }
}
