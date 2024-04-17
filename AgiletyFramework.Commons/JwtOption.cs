namespace AgiletyFramework.Commons;

public class JwtOptions
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public string? SigningKey { get; set; }
}
