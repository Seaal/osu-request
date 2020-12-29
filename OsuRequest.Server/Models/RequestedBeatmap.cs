using OsuRequest.OsuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRequest.Server.Models
{
    public class RequestedBeatmap
    {
        public int UserId { get; set; }
        public Beatmap Beatmap { get; set; }
    }
}
