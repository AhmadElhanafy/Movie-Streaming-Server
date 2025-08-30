// Domain/Person.cs
using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class Person
{
    public Guid Id { get; set; }
    [MaxLength(256)] public string Name { get; set; } = string.Empty;

    [MaxLength(32)] public string? TmdbId { get; set; }
    [MaxLength(32)] public string? ImdbId { get; set; }

    public ICollection<MediaPerson> Credits { get; set; } = new List<MediaPerson>();

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
