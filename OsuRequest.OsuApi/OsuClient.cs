using OsuRequest.CrossCutting;
using OsuRequest.OsuApi.DTOs;
using OsuRequest.OsuApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace OsuRequest.OsuApi
{
    public class OsuClient : IOsuClient
    {
        private string accessToken;

        private readonly HttpClient osuHttpClient;
        private readonly HttpClient authHttpClient;

        private readonly JsonSerializerOptions snakeCaseSerializerOptions;

        public OsuClient()
        {
            osuHttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://osu.ppy.sh")
            };

            authHttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://osurequestauth.azurewebsites.net")
            };

            snakeCaseSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };
        }

        public async Task RequestTokenAsync(string code)
        {
            OsuTokenRequestDTO osuTokenRequestDto = new OsuTokenRequestDTO()
            {
                Code = code
            };

            HttpResponseMessage response = await authHttpClient.PostAsync("/auth/osu/token", JsonContent.Create(osuTokenRequestDto));

            if (response.IsSuccessStatusCode)
            {
                AuthResponseDTO authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                accessToken = authResponse.AccessToken;

                osuHttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        public async Task<Beatmap> GetBeatmapAsync(int id)
        {
            HttpResponseMessage response = await osuHttpClient.GetAsync($"/api/v2/beatmaps/{id}");

            if (response.IsSuccessStatusCode)
            {
                BeatmapDTO beatmapDto = await response.Content.ReadFromJsonAsync<BeatmapDTO>(snakeCaseSerializerOptions);

                return new Beatmap()
                {
                    Id = beatmapDto.Id,
                    Author = beatmapDto.Beatmapset.Artist,
                    SongName = beatmapDto.Beatmapset.Title,
                    DifficultyName = beatmapDto.Version,
                    DifficultyRating = beatmapDto.DifficultyRating,
                    Mode = ToBeatmapMode(beatmapDto.Mode),
                    RankedStatus = ToRankedStatus(beatmapDto.Ranked)
                };
            }

            throw new Exception();
        }

        private BeatmapRankedStatus ToRankedStatus(int ranked)
        {
            switch(ranked)
            {
                case -2:
                    return BeatmapRankedStatus.Graveyard;
                case -1:
                    return BeatmapRankedStatus.WorkInProgress;
                case 0:
                    return BeatmapRankedStatus.Pending;
                case 1:
                    return BeatmapRankedStatus.Ranked;
                case 2:
                    return BeatmapRankedStatus.Approved;
                case 3:
                    return BeatmapRankedStatus.Qualified;
                case 4:
                    return BeatmapRankedStatus.Loved;
                default:
                    return BeatmapRankedStatus.Unknown;
            }
        }

        private BeatmapMode ToBeatmapMode(string mode)
        {
            switch (mode)
            {
                case "osu":
                    return BeatmapMode.Osu;
                case "taiko":
                    return BeatmapMode.Taiko;
                case "fruit":
                    return BeatmapMode.Catch;
                case "mania":
                    return BeatmapMode.Mania;
                default:
                    return BeatmapMode.Unknown;
            }
        }
    }
}
