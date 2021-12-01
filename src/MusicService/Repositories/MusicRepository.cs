using Microsoft.EntityFrameworkCore;
using MusicService.ControllerModels;
using MusicService.Models;
using MusicService.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//music-service requirment 1.5.0 Music service will contain a repository layer named Music Repository. This layer will interact with the database directly.
namespace MusicService.Repositories
{
    //music-service requirment 1.5.1 Music Repository will inherit from the interface IMusicRepository. IMusicRepository contains 4 functions: GetAsync(), GetAsync(recordNumber), CreateAsync(entry), Delete(entry).
    public class MusicRepository : IMusicRepository
    {

        private readonly MusicDbContext _context;

        public MusicRepository(MusicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //music-service requirment 1.5.2 The first method music repository implements is GetAsync(). This method makes a call to the database directly to fetch all entries in the music table and then returns the list.
        public IAsyncEnumerable<Models.Music> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync");
            try
            {
                //making call to database to get all entries in music table
                var result = _context.Music.AsAsyncEnumerable();

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

        //music-service requirment 1.5.3 The second method music repository implements is GetAsync(recordNumber). This method makes a call to the database directly to fetch the entry that corresponds to the record number entered in by the user. The method then returns the entry.
        public async Task<Models.Music> GetAsync(long recordNumber)
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync - record number {recordNumber}");

            //making call to database to find music record with the specified record number
            var result = await _context.Music.FirstOrDefaultAsync(I => I.RecordNumber == recordNumber);

            System.Diagnostics.Debug.WriteLine($"Exiting GetAsync - record number {recordNumber}");

            return result;
        }

        //music-service requirment 1.5.4 The third method music repository implements is CreateAsync(entry). This method makes a new entry directly into the database. The entry is the music object entered in by the user and passed through the service layer to the repository. The new entry is added to the music table and then returned.
        public async Task<Models.Music> CreateAsync(MusicCreate create)
        {
            System.Diagnostics.Debug.WriteLine($"Entering CreateAsync - Music {create.Track}, {create.Artist}");

            //calculating and assigning new record number for entry
            var recordNumber = (_context.Music.Max(track => track.RecordNumber as long?) ?? 0) + 1;

            //creating the music object and assigning values
            var entity = new Models.Music
            {
                RecordNumber = recordNumber,
                Artist = create.Artist,
                Album = create.Album,
                Playlist = create.Playlist,
                Track = create.Track
            };

            try
            {
                //generating new guid for recordId
                entity.RecordId = Guid.NewGuid();

                //making call to database
                await _context.Music.AddAsync(entity);

                //saving changes to database
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
            }
        }

        //music-service requirment 1.5.5 The fourth method music repository implements is Delete(entry). This method deletes the music entry that is passed in through the service layer once it’s found by it’s record number that the user entered in. This method interacts directly with the database to remove the entry from the music table. The method then returns true or false if entry was deleted successfully.
        public async Task<bool> Delete(Models.Music track)
        {
            System.Diagnostics.Debug.WriteLine($"Entering Delete");
            try
            {
                //making call to database
                 _context.Music.Remove(track);

                //saving changes
                var numStateEntriesWritten = await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"Exiting Delete");
                // We should encounter a DbUpdateConcurrencyException if an unexpected number of rows were affected
                return true;
            }
            catch (DbUpdateConcurrencyException dbuce)
            {
                // This means not all of the records existed to be deleted
                System.Diagnostics.Debug.WriteLine(dbuce, $"Failed to delete entity - concurrent access");
                return false;
            }
            catch (DbUpdateException dbue)
            {
                System.Diagnostics.Debug.WriteLine(dbue, $"Failed to delete entity");
                // We want to re-throw this error, as it is indicative of something wrong with the database
                // which would be an internal server issue
                throw;
            }
        }
    }
}
