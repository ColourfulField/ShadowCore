namespace ShadowCore.Common.Options
{
    public class AuthenticationOptions
    {
        public BearerAuthenticationOptions Bearer { get; set; }
    }

    public class BearerAuthenticationOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
        public string AllowRefreshTokenRenew { get; set; }
        #warning Use your own secret store, do not keep it in appsettings and in source control
        public string Secret { get; set; }
    }
}