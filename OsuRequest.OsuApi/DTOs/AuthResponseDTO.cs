using System;
using System.Collections.Generic;
using System.Text;

namespace OsuRequest.OsuApi.DTOs
{
    public class AuthResponseDTO
    {
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
