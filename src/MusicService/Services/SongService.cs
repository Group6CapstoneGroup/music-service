using MusicService.ControllerModels;
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

        public async Task<Models.Music> CreateAsync(MusicCreate create)
        {
            System.Diagnostics.Debug.WriteLine("Entering CreateAsync");

            var entity = await _repository.CreateAsync(create);
            if (entity == null)
            {
                return null;
            }
            System.Diagnostics.Debug.WriteLine($"Exiting CreateAsync - ID {entity.RecordId}");
            return entity;
        }

        public async Task<bool> DeleteAsync(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine($"Entering DeleteAsync");
            var track = GetAsync(recordNumber).Result;
            var removed = await _repository.Delete(track);

            System.Diagnostics.Debug.WriteLine($"Exiting DeleteAsync");
            return removed;
        }

        public IAsyncEnumerable<Models.Music> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine("Entering GetAsync");
            var result = _repository.GetAsync();
            System.Diagnostics.Debug.WriteLine("Exiting GetAsync");
            return (IAsyncEnumerable<Models.Music>)result;
        }

        public async Task<Models.Music> GetAsync(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync - record number {recordNumber}");
            var entity = await _repository.GetAsync(recordNumber);
            System.Diagnostics.Debug.WriteLine($"Exiting GetAsync - record number {recordNumber}");
            // entity is null if not found
            return entity;
        }
    }
}
