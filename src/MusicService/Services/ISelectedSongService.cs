using MusicService.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    public interface ISelectedSongService
    {
        IAsyncEnumerable<Models.SelectedSong> GetSelectedSongs();

        Task<Models.SelectedSong> CreateSongRecord(ControllerModels.SelectedSongCreate entry);
    }
}
