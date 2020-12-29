using OsuRequest.Server.DTOs;
using OsuRequest.Server.Models;
using System.Threading.Tasks;

namespace OsuRequest.Server.Services
{
    public interface IRequestService
    {
        Task<RequestedBeatmap> CreateRequestAsync(RequestDTO requestDto);
        void ClearRequests();
    }
}
