using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class MediaItem
{
    public Guid Id { get; set; }

    [MaxLength(512)] public string Title { get; set; } = string.Empty;
    public MediaKind Kind { get; set; } = MediaKind.Movie;

    // Optional series info (for future-proofing)
    [MaxLength(512)] public string? ShowTitle { get; set; }
    public int? SeasonNumber { get; set; }
    public int? EpisodeNumber { get; set; }

    // Original source file on disk
    [MaxLength(2048)] public string SourcePath { get; set; } = string.Empty;
    public long SourceSizeBytes { get; set; }
    [MaxLength(64)] public string? SourceVideoCodec { get; set; }
    [MaxLength(64)] public string? SourceAudioCodec { get; set; }
    public int? SourceWidth { get; set; }
    public int? SourceHeight { get; set; }
    public double? SourceFrameRate { get; set; }
    public TimeSpan? Duration { get; set; }

    // External ids (helpful when I add TMDb later)
    [MaxLength(32)] public string? TmdbId { get; set; }
    [MaxLength(32)] public string? ImdbId { get; set; }
    public DateOnly? ReleaseDate { get; set; }

    // Navigation
    public Guid LibraryPathId { get; set; }
    public LibraryPath LibraryPath { get; set; } = default!;
    public ICollection<StreamRendition> Renditions { get; set; } = new List<StreamRendition>();
    public ICollection<MediaPerson> People { get; set; } = new List<MediaPerson>();

    // Audit
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
