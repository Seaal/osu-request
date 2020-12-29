using System;
using System.Collections.Generic;
using System.Text;

namespace OsuRequest.OsuApi.DTOs
{
    public class AuthRequestDTO
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Code { get; set; }
        public string GrantType { get; set; }
        public string RedirectUri { get; set; }
    }
}
