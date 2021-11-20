using Microsoft.EntityFrameworkCore;
using MusicService.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    public class SelectedSongRepository : ISelectedSongRepository
    {

        private readonly MusicDbContext _context;

        public SelectedSongRepository(MusicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Models.SelectedSong> CreateAsync(ControllerModels.SelectedSongCreate create)
        {
            System.Diagnostics.Debug.WriteLine($"Entering CreateAsync - Music {create.Track}, {create.Artist}");
            var recordNumber = (_context.SelectedSong.Max(song => song.RecordNumber as long?) ?? 0) + 1;

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
                entity.RecordId = Guid.NewGuid();
                await _context.SelectedSong.AddAsync(entity);
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

        public IAsyncEnumerable<Models.SelectedSong> GetAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync");
            try
            {
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
    }
}
