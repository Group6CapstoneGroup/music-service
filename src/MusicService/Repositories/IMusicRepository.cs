using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    public interface IMusicRepository
    {
        IAsyncEnumerable<Music> GetAsync();
        Task<Music> GetAsync(string trackName, string artist, string album, string playlist);
        Task<Music> CreateAsync(string trackName, string artist, string album, string playlist);
        Task<bool> Delete(string trackName, string artist, string album, string playlist);
    }
}
