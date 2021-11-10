using MusicService.Models;
using MusicService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    public class SongService : IMusicService
    {
        private readonly IMusicRepository _repository;

        public SongService(IMusicRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<Music> CreateAsync(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Music> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine("Entering GetAsync");
            var result = _repository.GetAsync();
            System.Diagnostics.Debug.WriteLine("Exiting GetAsync");
            return result;
        }

        public Task<Music> GetAsync(string trackName)
        {
            throw new NotImplementedException();
        }
    }
}
