using System.Text.Json.Serialization;

namespace DapperASPNetCore.Dto
{
    public class UserLoginDto
    {
        // private int id { get; set; }

        public string Username { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public string? UserUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EntityId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public string? SaltKey { get; set; }
        public DateTime RegistrationDateUtc { get; set; }

    }
}
