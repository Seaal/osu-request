using System;
using System.Collections.Generic;
using System.Text;

namespace OsuRequest.OsuApi.Models
{
    [Flags]
    public enum BeatmapRankedStatus
    {
        Unknown,
        Graveyard,
        WorkInProgress,
        Pending,
        Ranked,
        Approved,
        Qualified,
        Loved
    }
}
