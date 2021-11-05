using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        public Task<Music> CreateAsync(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Music> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Music> GetAsync(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }
    }
}
