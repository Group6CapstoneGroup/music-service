using MusicService.ControllerModels;
using MusicService.Models;
using MusicService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//music-service requirment 1.3.0 Music service will contain a service called song service.
namespace MusicService.Services
{
    //music service requirment 1.3.1 Song service will inherit from an interface called IMusicService. This gives song service template it needs to satisfy for all its actions. IMusicService contains four functions: GetAsync(),GetAsync(recordNumber), CreateAsync(entry), DeleteAsync(recordNumber).
    public class SongService : IMusicService
    {
        private readonly IMusicRepository _repository;

        public SongService(IMusicRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //music-service requirment 1.3.2 The first method song service implements is GetAsync(). This method makes a call to the music repository to fetch a list directly from the database of all current entries in the music table.

        public IAsyncEnumerable<Models.Music> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine("Entering GetAsync");

            //making call to repository
            var result = _repository.GetAsync();
           
            System.Diagnostics.Debug.WriteLine("Exiting GetAsync");
            
            return (IAsyncEnumerable<Models.Music>)result;
        }

        //music-service requirment 1.3.3 The second method song service implements is called GetAsync(recordNumber). This method makes a call to the music repository to fetch the music entry from the database with the corresponding record number entered by the user.
        public async Task<Models.Music> GetAsync(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync - record number {recordNumber}");

            //making call to repository
            var entity = await _repository.GetAsync(recordNumber);
            
            System.Diagnostics.Debug.WriteLine($"Exiting GetAsync - record number {recordNumber}");
            
            // entity is null if not found
            return entity;
        }

        //music-service requirment 1.3.4 The third method song service implements is called CreateAsync(entry). This method makes a call to the music repository to create a new music entry within the music table of the database. The entry is the input the user has passed in with the following attributes: track, artist, album, playlist.
        public async Task<Models.Music> CreateAsync(MusicCreate create)
        {
            System.Diagnostics.Debug.WriteLine("Entering CreateAsync");

            //making call to repository
            var entity = await _repository.CreateAsync(create);

            //check to make sure if null was returned from the repository, if it was return null entity not created
            if (entity == null)
            {
                return null;
            }
            System.Diagnostics.Debug.WriteLine($"Exiting CreateAsync - ID {entity.RecordId}");

            //else return newly created entity
            return entity;
        }

        //music-service requirment 1.3.5 The fourth method song service implements is called DeleteAsync(recordNumber). This method makes a call to the music repository to delete the music record with the inputted record number from the user.
        public async Task<bool> DeleteAsync(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine($"Entering DeleteAsync");
            
            //make a call to SongService to get the music object associated with the record number
            var track = GetAsync(recordNumber).Result;

            //making call to repository passing in the music object
            var removed = await _repository.Delete(track);

            System.Diagnostics.Debug.WriteLine($"Exiting DeleteAsync");
            
            return removed;
        }
    }
}
