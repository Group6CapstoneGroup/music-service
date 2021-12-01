using MusicService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//music-service requirment 1.4.0 Music service will contain a service called selected song service.
namespace MusicService.Services
{
    public class SelectedSongService : ISelectedSongService
    {
        //music-service requirment 1.4.1 Selected song service will inherit from an interface called ISelectedSongService. This gives song service template it needs to satisfy for all its actions. IMusicService contains three functions: GetSelectedSongs(), CreateSongRecord(entry), DeleteAsync().
        private readonly ISelectedSongRepository _repository;

        public SelectedSongService(ISelectedSongRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //music-service requirment 1.4.2 The first method song service implements is GetSelectedSongs(). This method makes a call to the selected song repository to fetch a list directly from the database of all current entries in the selected song table.
        public IAsyncEnumerable<Models.SelectedSong> GetSelectedSongs()
        {
            System.Diagnostics.Debug.WriteLine("Entering GetAsync");

            //making call to song service repository
            var result = _repository.GetAsync();

            System.Diagnostics.Debug.WriteLine("Exiting GetAsync");

            return (IAsyncEnumerable<Models.SelectedSong>)result;
        }

        //music-service requirment 1.4.3 The second method selected song service implements is called CreateSongRecord(entry). This method makes a call to selected song repository to create a new entry in the selected song table within the database. The song entry attributes are entered in by the user and include: track, artist, album, playlist.
        public async Task<Models.SelectedSong> CreateSongRecord(ControllerModels.SelectedSongCreate entry)
        {
            System.Diagnostics.Debug.WriteLine("Entering CreateAsync");

            //making call to song service repository
            var entity = await _repository.CreateAsync(entry);

            //check if entity is null if it is no entry was created in database
            if (entity == null)
            {
                return null;
            }

            System.Diagnostics.Debug.WriteLine($"Exiting CreateAsync - ID {entity.RecordId}");
            
            return entity;
        }

        //music-service requirment 1.4.4 The third method selected song service implements is called DeleteAsync(). This method makes a call to the selected song repository and deletes all rows within selected song table in the database.
        public async Task<bool> DeleteAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Entering DeleteAsync");

            //making call to get a list of all selected song entries we want to delete
            var selectedSongList = GetSelectedSongs();

            //making call to song service repository and passing in a list of all entries we are deleting from the database
            var removed = await _repository.DeleteAsync(selectedSongList);

            System.Diagnostics.Debug.WriteLine($"Exiting DeleteAsync");

            return removed;
        }
    }
}
