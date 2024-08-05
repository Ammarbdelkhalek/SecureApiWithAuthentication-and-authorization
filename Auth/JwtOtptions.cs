
public class JwtOtptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int LifeTime { get; set; }
    public string signinKey { get; set; }
}
 