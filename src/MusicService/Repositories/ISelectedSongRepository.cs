using MusicService.ControllerModels;
using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    //music-service requirment 1.6.1 Selected Song Repository will inherit from the interface ISelectedSongRepository. ISelectedSongRepository contains 3 functions: GetAsync(), CreateAsync(entry), Delete(entries).
    public interface ISelectedSongRepository
    {
        IAsyncEnumerable<Models.SelectedSong> GetAsync();
        Task<Models.SelectedSong> CreateAsync(SelectedSongCreate entry);
        Task<bool> DeleteAsync(IAsyncEnumerable<SelectedSong> keys);
    }
}
