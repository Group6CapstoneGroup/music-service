using MusicService.ControllerModels;
using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Repositories
{
    //music-service requirment 1.5.1 Music Repository will inherit from the interface IMusicRepository. IMusicRepository contains 4 functions: GetAsync(), GetAsync(recordNumber), CreateAsync(entry), Delete(entry).
    public interface IMusicRepository
    {
        IAsyncEnumerable<Models.Music> GetAsync();
        Task<Models.Music> GetAsync(long recordNumber);
        Task<Models.Music> CreateAsync(MusicCreate entry);
        Task<bool> Delete(Models.Music track);
    }
}
