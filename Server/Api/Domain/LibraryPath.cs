// Domain/LibraryPath.cs
using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class LibraryPath
{
    public Guid Id { get; set; }
    [MaxLength(2048)] public string Path { get; set; } = string.Empty; // host path mounted into worker
    public bool Enabled { get; set; } = true;

    [MaxLength(1024)] public string? IncludePattern { get; set; } // e.g. *.mkv;*.mp4
    [MaxLength(1024)] public string? ExcludePattern { get; set; }

    public DateTimeOffset? LastScanStartedAtUtc { get; set; }
    public DateTimeOffset? LastScanCompletedAtUtc { get; set; }

    public ICollection<MediaItem> Media { get; set; } = new List<MediaItem>();
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
