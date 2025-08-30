using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class StreamRendition
{
    public Guid Id { get; set; }
    public Guid MediaItemId { get; set; }
    public MediaItem MediaItem { get; set; } = default!;

    public TranscodeStatus Status { get; set; } = TranscodeStatus.Pending;

    // ABR attributes
    public int Width { get; set; }
    public int Height { get; set; }
    public int BitrateKbps { get; set; }
    public double? FrameRate { get; set; }
    [MaxLength(64)] public string VideoCodec { get; set; } = "h264";
    [MaxLength(64)] public string AudioCodec { get; set; } = "aac";
    public ContainerFormat Container { get; set; } = ContainerFormat.Fmp4;

    // HLS/DASH artifacts (in MinIO / S3)
    // Key prefix for all segments of this rendition (e.g. "media/{mediaId}/hls/1080p/")
    [MaxLength(1024)] public string ObjectKeyPrefix { get; set; } = string.Empty;
    // Manifest path (e.g. "media/{mediaId}/hls/1080p/stream.m3u8")
    [MaxLength(1024)] public string ManifestObjectKey { get; set; } = string.Empty;

    public int? SegmentDurationSeconds { get; set; }
    public int? SegmentCount { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
