using System;
using System.Collections.Generic;
using System.Text;

namespace OsuRequest.OsuApi.Models
{
    [Flags]
    public enum BeatmapMode
    {
        Unknown,
        Osu,
        Taiko,
        Catch,
        Mania
    }
}
