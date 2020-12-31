namespace OsuRequest.AuthServer.DTOs
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
