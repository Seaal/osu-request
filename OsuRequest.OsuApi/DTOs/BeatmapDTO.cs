namespace OsuRequest.OsuApi.DTOs
{
    public class BeatmapDTO
    {
        public int Id { get; set; }
        public double DifficultyRating { get; set; }
        public string Mode { get; set; }
        public string Version { get; set; }
        public int Ranked { get; set; }
        public BeatmapSetDTO Beatmapset { get; set; }
    }
}
