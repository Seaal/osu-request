namespace OsuRequest.CrossCutting
{
    public class AuthResponseDTO
    {
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
