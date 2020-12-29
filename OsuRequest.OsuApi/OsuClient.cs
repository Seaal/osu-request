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

        private readonly HttpClient httpClient;

        private readonly JsonSerializerOptions snakeCaseSerializerOptions;

        public OsuClient()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://osu.ppy.sh")
            };

            snakeCaseSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };
        }

        public async Task RequestTokenAsync(string code)
        {
            AuthRequestDTO authRequestDto = new AuthRequestDTO()
            {
                ClientId = 4309,
                ClientSecret = "INSERT CLIENT SECRET HERE",
                Code = code,
                GrantType = "authorization_code",
                RedirectUri = "http://localhost:61899/osu/callback"
            };

            HttpResponseMessage response = await httpClient.PostAsync("/oauth/token", JsonContent.Create(authRequestDto, null, snakeCaseSerializerOptions));

            if (response.IsSuccessStatusCode)
            {
                AuthResponseDTO authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(snakeCaseSerializerOptions);

                accessToken = authResponse.AccessToken;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        public async Task<Beatmap> GetBeatmapAsync(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"/api/v2/beatmaps/{id}");

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
