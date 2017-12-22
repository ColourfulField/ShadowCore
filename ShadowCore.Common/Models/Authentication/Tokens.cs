namespace ShadowCore.Common.Models.Authentication
{
    public class AuthorizationTokens
    {
        public AuthToken AccessToken { get; set; }
        public AuthToken RefreshToken { get; set; }
    }
}
