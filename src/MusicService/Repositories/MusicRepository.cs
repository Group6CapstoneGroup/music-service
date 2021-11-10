using MusicService.Models;
using MusicService.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    public class MusicRepository : IMusicRepository
    {

        private readonly MusicDbContext _context;

        public MusicRepository(MusicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
            System.Diagnostics.Debug.WriteLine($"Entering GetAsync");
            try
            {
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

        public Task<Music> GetAsync(string trackName, string artist, string album, string playlist)
        {
            throw new NotImplementedException();
        }
    }
}
