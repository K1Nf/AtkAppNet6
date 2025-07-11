﻿namespace ATKApp6.Infrastructure.Extensions
{
    public class JWTConfiguration
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 60;
        public string SecretKey { get; set; } = string.Empty;
        public string OrganizationId { get; set; } = string.Empty;

    }
}
