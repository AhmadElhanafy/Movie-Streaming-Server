using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Api.Domain;

public class User
{
    public Guid Id { get; set; }

    [MaxLength(64)] public string Username { get; set; } = string.Empty;
    [MaxLength(128)] public string DisplayName { get; set; } = string.Empty;

    // Password hash & salt
    [MaxLength(256)] public string PasswordHash { get; set; } = string.Empty;
    [MaxLength(256)] public string PasswordSalt { get; set; } = string.Empty;

    // Role / claims
    [MaxLength(32)] public string Role { get; set; } = "User";

    public ICollection<PlaybackSession> Sessions { get; set; } = new List<PlaybackSession>();
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    // ===== Helper methods for password hashing =====
    public void SetPassword(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordSalt = Convert.ToBase64String(hmac.Key);
        PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password)
    {
        var key = Convert.FromBase64String(PasswordSalt);
        using var hmac = new HMACSHA512(key);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return PasswordHash == Convert.ToBase64String(computedHash);
    }
}
