using System.Text.Json.Serialization;

// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-ignore-properties?pivots=dotnet-6-0

namespace DapperASPNetCore.Dto
{
    public class AccountDto
    {
        // private int id { get; set; }

        public string Username { get; set; }
        [JsonIgnore]
        public string? UserUID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string? SaltKey { get; set; } 
        public DateTime RegistrationDateUtc { get; set; }

    }
}
