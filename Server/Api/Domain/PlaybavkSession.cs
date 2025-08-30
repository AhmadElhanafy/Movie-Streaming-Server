// Domain/PlaybackSession.cs
using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class PlaybackSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid MediaItemId { get; set; }
    public MediaItem MediaItem { get; set; } = default!;

    public Guid? StreamRenditionId { get; set; } // selected ladder
    public StreamRendition? StreamRendition { get; set; }

    public PlaybackType PlaybackType { get; set; } = PlaybackType.DirectPlay;
    public double PositionSeconds { get; set; } // last position
    public bool Completed { get; set; }

    [MaxLength(128)] public string? Device { get; set; } // e.g. "Chrome on Windows"
    [MaxLength(64)] public string? ClientVersion { get; set; }
    [MaxLength(64)] public string? IpAddress { get; set; }

    public DateTimeOffset StartedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastUpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? EndedAtUtc { get; set; }
}
