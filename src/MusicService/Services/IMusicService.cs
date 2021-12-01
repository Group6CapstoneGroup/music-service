using MusicService.ControllerModels;
using MusicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Services
{
    //nusic-service requirment 1.3.1 Song service will inherit from an interface called IMusicService. This gives song service template it needs to satisfy for all its actions. IMusicService contains four functions: GetAsync(),GetAsync(recordNumber), CreateAsync(entry), DeleteAsync(recordNumber).
    public interface IMusicService
    {
        IAsyncEnumerable<Models.Music> GetAsync();
        Task<Models.Music> GetAsync(long recordNumber);

        Task<Models.Music> CreateAsync(MusicCreate entry);

        Task<bool> DeleteAsync(long recordNumber);
    }
}
