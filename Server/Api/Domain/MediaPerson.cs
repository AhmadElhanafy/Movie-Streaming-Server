// Domain/MediaPerson.cs  (join table)
using System.ComponentModel.DataAnnotations;

namespace Api.Domain;

public class MediaPerson
{
    public Guid MediaItemId { get; set; }
    public MediaItem MediaItem { get; set; } = default!;

    public Guid PersonId { get; set; }
    public Person Person { get; set; } = default!;

    public PersonRole Role { get; set; } = PersonRole.Cast;
    [MaxLength(256)] public string? CharacterName { get; set; }
    public int? BillingOrder { get; set; }
}
