using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OsuRequest.AuthServer.DTOs;
using OsuRequest.CrossCutting;

namespace OsuRequest.AuthServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://osu.ppy.sh")
        };

        private static readonly JsonSerializerOptions snakeCaseSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        };

        [HttpPost("osu/token")]
        public async Task<AuthResponseDTO> RequestTokenAsync([FromBody] OsuTokenRequestDTO tokenRequestDto)
        {
            // To auth a code - https://osu.ppy.sh/oauth/authorize?client_id=4309&redirect_uri=http://localhost:61899/osu/callback&response_type=code&scope=identify public&state=chicken

            string clientSecret = Environment.GetEnvironmentVariable("OsuRequest_Osu_ClientSecret");

            if (clientSecret == null)
            {
                throw new HttpResponseException();
            }

            AuthRequestDTO authRequestDto = new AuthRequestDTO()
            {
                ClientId = 4309,
                ClientSecret = clientSecret,
                Code = tokenRequestDto.Code,
                GrantType = "authorization_code",
                RedirectUri = "http://localhost:61899/osu/callback"
            };

            HttpResponseMessage response = await httpClient.PostAsync("/oauth/token", JsonContent.Create(authRequestDto, null, snakeCaseSerializerOptions));

            if (response.IsSuccessStatusCode)
            {
                AuthResponseDTO authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(snakeCaseSerializerOptions);

                return authResponse;
            }

            throw new HttpResponseException()
            {
                Status = (int)response.StatusCode,
                Value = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
