using OsuRequest.OsuApi;
using OsuRequest.OsuApi.Models;
using OsuRequest.Server.DTOs;
using OsuRequest.Server.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace OsuRequest.Server.Services
{
    public class RequestService : IRequestService
    {
        private readonly IOsuClient osuClient;
        
        private readonly ConcurrentQueue<RequestedBeatmap> beatmapQueue = new ConcurrentQueue<RequestedBeatmap>();

        public RequestService(IOsuClient osuClient)
        {
            this.osuClient = osuClient;
        }

        public async Task<RequestedBeatmap> CreateRequestAsync(RequestDTO requestDto)
        {
            Beatmap beatmap = await osuClient.GetBeatmapAsync(requestDto.BeatmapId);

            RequestedBeatmap requestedBeatmap = new RequestedBeatmap()
            {
                Beatmap = beatmap,
                UserId = requestDto.UserId
            };

            beatmapQueue.Enqueue(requestedBeatmap);

            return requestedBeatmap;
        }

        public void ClearRequests()
        {
            beatmapQueue.Clear();
        }


    }
}
