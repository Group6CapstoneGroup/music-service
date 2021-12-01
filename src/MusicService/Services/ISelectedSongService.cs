using MusicService.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    //music-service requirment 1.4.1 Selected song service will inherit from an interface called ISelectedSongService. This gives song service template it needs to satisfy for all its actions. IMusicService contains three functions: GetSelectedSongs(), CreateSongRecord(entry), DeleteAsync().
    public interface ISelectedSongService
    {
        IAsyncEnumerable<Models.SelectedSong> GetSelectedSongs();

        Task<Models.SelectedSong> CreateSongRecord(ControllerModels.SelectedSongCreate entry);

        Task<bool> DeleteAsync();

    }
}
