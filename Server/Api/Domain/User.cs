using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class User
{
    public Guid Id { get; set; }
    [MaxLength(64)] public string Username { get; set; } = string.Empty;
    [MaxLength(128)] public string DisplayName { get; set; } = string.Empty;

    // For external IdP later (Keycloak/Identity): map OIDC 'sub'
    [MaxLength(256)] public string? ExternalId { get; set; }

    public ICollection<PlaybackSession> Sessions { get; set; } = new List<PlaybackSession>();

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
