using Microsoft.EntityFrameworkCore;
using MusicService.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//music-service requirment 1.6.0 Music service will contain a repository layer named Selected Song Repository. This layer will interact with the database directly.
namespace MusicService.Repositories
{

    //music-service requirment 1.6.1 Selected Song Repository will inherit from the interface ISelectedSongRepository. ISelectedSongRepository contains 3 functions: GetAsync(), CreateAsync(entry), Delete(entries).
    public class SelectedSongRepository : ISelectedSongRepository
    {

        private readonly MusicDbContext _context;

        public SelectedSongRepository(MusicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //music-service requirment 1.6.2 The first method selected song repository implements is GetAsync(). This method makes a call to the database directly to fetch all entries in the selected song table and then returns the list.
        public IAsyncEnumerable<Models.SelectedSong> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync");
            try
            {
                //making call to database to get all entries from selected song table
                var result = _context.SelectedSong.AsAsyncEnumerable();

                System.Diagnostics.Debug.WriteLine($"Exiting GetAsync");
                
                return result;
            }
            catch (InvalidOperationException ioe)
            {
                System.Diagnostics.Debug.WriteLine(ioe, "Data source was null - could not get async enumerable");
                // We want to re-throw this error, as it is indicative of something wrong with the database
                // which would be an internal server issue
                throw;
            }
            catch (ArgumentNullException ane)
            {
                System.Diagnostics.Debug.WriteLine(ane, "Data source was not an IAsyncEnumerable - could not get async enumerable");
                // We want to re-throw this error, as it is indicative of something wrong with the database
                // which would be an internal server issue
                throw;
            }
        }

        //music-service requirment 1.6.3 The second method selected song repository implements is CreateAsync(entry). This method makes a new entry directly into the database. The entry is the selected song object entered in by the user and passed through the service layer to the repository. The new entry is added to the selected song table and then returned.
        public async Task<Models.SelectedSong> CreateAsync(ControllerModels.SelectedSongCreate create)
        {
            System.Diagnostics.Debug.WriteLine($"Entering CreateAsync - Music {create.Track}, {create.Artist}");

            //calculating the next record number available to assign to the new entry we are about to create
            var recordNumber = (_context.SelectedSong.Max(song => song.RecordNumber as long?) ?? 0) + 1;

            //creating selected song object and assigning values
            var entity = new Models.SelectedSong
            {
                RecordNumber = recordNumber,
                Artist = create.Artist,
                Album = create.Album,
                Playlist = create.Playlist,
                Track = create.Track
            };

            try
            {
                //generating new guid for the recordID
                entity.RecordId = Guid.NewGuid();

                //adding new entry to the database
                await _context.SelectedSong.AddAsync(entity);

                //saving database
                await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"Exiting CreateAsync - RecordNumber {recordNumber}, music {entity.Track}, {entity.Artist}");
                // We should encounter a DbUpdateConcurrencyException if an unexpected number of rows were affected

                return entity;
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                System.Diagnostics.Debug.WriteLine(dbuce, $"Failed to create entity - RecordNumber {recordNumber}, music {entity.Track}, {entity.Artist} - concurrent access");
                return null;
            }
            catch (DbUpdateException dbue)
            {
                System.Diagnostics.Debug.WriteLine(dbue, $"Failed to create entity - RecordNumber {recordNumber}, music {entity.Track}, {entity.Artist}");
                // We want to re-throw this error, as it is indicative of something wrong with the database
                // which would be an internal server issue
                throw;
            }        }

        //music-service requirment 1.6.4 The third method music repository implements is Delete(entries). This method deletes the selected song list that is passed in through the service layer. This method interacts directly with the database to remove all entries from the selected table. The method then returns true or false if entry was deleted successfully.
        public async Task<bool> DeleteAsync(IAsyncEnumerable<Models.SelectedSong> songKeys)
        {
            System.Diagnostics.Debug.WriteLine($"Entering DeleteAsync multiple");
            try
            {
                await foreach (var song in songKeys)
                {
                    //making call to database
                    _context.SelectedSong.Remove(song);
                }

                //saving state of the database
                var numStateEntriesWritten = await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"Deleted {numStateEntriesWritten} entities");

                System.Diagnostics.Debug.WriteLine($"Exiting DeleteAsync multiple");

                // We should encounter a DbUpdateConcurrencyException if an unexpected number of rows were affected
                return true;
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                // This means not all of the records existed to be deleted
                System.Diagnostics.Debug.WriteLine(dbuce, $"Failed to delete entities - concurrent access");
                return false;
            }
            catch (DbUpdateException dbue)
            {
                System.Diagnostics.Debug.WriteLine(dbue, $"Failed to delete entities");
                // We want to re-throw this error, as it is indicative of something wrong with the database
                // which would be an internal server issue
                throw;
            }
        }
    }
}
