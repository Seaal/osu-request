using System;
using System.Collections.Generic;
using System.Text;

namespace OsuRequest.OsuApi.Models
{
    public class Beatmap
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string Author { get; set; }
        public BeatmapMode Mode { get; set; }
        public BeatmapRankedStatus RankedStatus { get; set; }
        public string DifficultyName { get; set; }
        public double DifficultyRating { get; set; }

    }
}
