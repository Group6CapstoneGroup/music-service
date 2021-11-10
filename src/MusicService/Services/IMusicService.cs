using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    public interface IMusicService
    {
        IAsyncEnumerable<Music> GetAsync();
        Task<Music> GetAsync(string trackName);

        Task<Music> CreateAsync(string trackName, string artist, string album, string playlist);

        Task<bool> DeleteAsync(string trackName, string artist, string album, string playlist);
    }
}
