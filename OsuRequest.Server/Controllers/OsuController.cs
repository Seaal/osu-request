using Microsoft.AspNetCore.Mvc;
using OsuRequest.OsuApi;
using System.Threading.Tasks;

namespace OsuRequest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OsuController : ControllerBase
    {
        private readonly IOsuClient osuClient;

        public OsuController(IOsuClient osuClient)
        {
            this.osuClient = osuClient;
        }

        [HttpGet("callback")]
        public async Task AuthCallbackAsync([FromQuery] string code)
        {
            await osuClient.RequestTokenAsync(code);
        }
    }
}
