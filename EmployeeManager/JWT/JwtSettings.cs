namespace EmployeeManage.JWT
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public string IssuerSigningKey { get; set; }

        public bool ValidateIssuer { get; set; } = true;
        public string ValidIssuer { get; set; }

        public bool ValidateAudience { get; set; } = true;
        public string ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;

        public double ExpireAccessToken { get; set; }
        public double ExpireRefreshToken { get; set; }
    }
}
