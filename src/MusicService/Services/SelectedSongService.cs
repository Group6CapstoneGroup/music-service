using MusicService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    public class SelectedSongService : ISelectedSongService
    {
        private readonly ISelectedSongRepository _repository;

        public SelectedSongService(ISelectedSongRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Models.SelectedSong> CreateSongRecord(ControllerModels.SelectedSongCreate entry)
        {
            System.Diagnostics.Debug.WriteLine("Entering CreateAsync");

            var entity = await _repository.CreateAsync(entry);
            if (entity == null)
            {
                return null;
            }
            System.Diagnostics.Debug.WriteLine($"Exiting CreateAsync - ID {entity.RecordId}");
            return entity;
        }

        public IAsyncEnumerable<Models.SelectedSong> GetSelectedSongs()
        {
            System.Diagnostics.Debug.WriteLine("Entering GetAsync");
            var result = _repository.GetAsync();
            System.Diagnostics.Debug.WriteLine("Exiting GetAsync");
            return (IAsyncEnumerable<Models.SelectedSong>)result;
        }
    }
}
