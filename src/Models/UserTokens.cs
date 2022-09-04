namespace WebApplication.Models;

public class UserTokens
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public TimeSpan Validaty { get; set; }
    public string RefreshToken { get; set; }
    public Guid Id { get; set; }
    public string EmailId { get; set; }
    public string EntityId { get; set; }
    public Guid GuidId { get; set; }
    public DateTime ExpiredTime { get; set; }

    // compatibility ecommerce:
    public string firstname  { get; set; }
    public string lastname { get; set; }
    public string email { get; set; }
    public string role { get; set; }

}

