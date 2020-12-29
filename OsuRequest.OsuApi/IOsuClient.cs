using OsuRequest.OsuApi.Models;
using System.Threading.Tasks;

namespace OsuRequest.OsuApi
{
    public interface IOsuClient
    {
        Task RequestTokenAsync(string code);
        Task<Beatmap> GetBeatmapAsync(int id);
    }
}
