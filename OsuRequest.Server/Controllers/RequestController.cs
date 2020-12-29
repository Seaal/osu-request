using Microsoft.AspNetCore.Mvc;
using OsuRequest.Server.DTOs;
using OsuRequest.Server.Models;
using OsuRequest.Server.Services;
using System.Threading.Tasks;

namespace OsuRequest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;

        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        [HttpPost]
        public async Task<RequestedBeatmap> CreateRequest([FromBody] RequestDTO requestDto)
        {
            return await requestService.CreateRequestAsync(requestDto);
        }
    }
}
