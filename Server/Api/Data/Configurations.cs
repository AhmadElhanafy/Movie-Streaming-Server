// Data/Configurations.cs
using Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data;

public class MediaItemConfig : IEntityTypeConfiguration<MediaItem>
{
    public void Configure(EntityTypeBuilder<MediaItem> e)
    {
        e.HasKey(x => x.Id);
        e.HasOne(x => x.LibraryPath).WithMany(x => x.Media).HasForeignKey(x => x.LibraryPathId).OnDelete(DeleteBehavior.Restrict);

        e.Property(x => x.Title).IsRequired();
        e.HasIndex(x => x.Title);
        e.HasIndex(x => new { x.Kind, x.ShowTitle, x.SeasonNumber, x.EpisodeNumber });
        e.HasIndex(x => x.TmdbId).IsUnique(false);
        e.HasIndex(x => x.ImdbId).IsUnique(false);
        e.Property(x => x.Duration).HasConversion<long?>(); // store ticks
        e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
        e.Property(x => x.UpdatedAtUtc).HasDefaultValueSql("NOW()");
    }
}

public class StreamRenditionConfig : IEntityTypeConfiguration<StreamRendition>
{
    public void Configure(EntityTypeBuilder<StreamRendition> e)
    {
        e.HasKey(x => x.Id);
        e.HasOne(x => x.MediaItem).WithMany(x => x.Renditions).HasForeignKey(x => x.MediaItemId).OnDelete(DeleteBehavior.Cascade);
        e.HasIndex(x => new { x.MediaItemId, x.Width, x.Height, x.BitrateKbps }).IsUnique();
        e.Property(x => x.Container).HasConversion<int>();
        e.Property(x => x.Status).HasConversion<int>();
        e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
        e.Property(x => x.UpdatedAtUtc).HasDefaultValueSql("NOW()");
    }
}

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Name).IsRequired();
        e.HasIndex(x => x.Name);
        e.HasIndex(x => x.TmdbId);
        e.HasIndex(x => x.ImdbId);
        e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
    }
}

public class MediaPersonConfig : IEntityTypeConfiguration<MediaPerson>
{
    public void Configure(EntityTypeBuilder<MediaPerson> e)
    {
        e.HasKey(x => new { x.MediaItemId, x.PersonId, x.Role });
        e.HasOne(x => x.MediaItem).WithMany(x => x.People).HasForeignKey(x => x.MediaItemId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(x => x.Person).WithMany(x => x.Credits).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
        e.Property(x => x.Role).HasConversion<int>();
        e.HasIndex(x => new { x.MediaItemId, x.Role, x.BillingOrder });
    }
}

public class LibraryPathConfig : IEntityTypeConfiguration<LibraryPath>
{
    public void Configure(EntityTypeBuilder<LibraryPath> e)
    {
        e.HasKey(x => x.Id);
        e.HasIndex(x => x.Path).IsUnique();
        e.Property(x => x.Path).IsRequired();
        e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
    }
}

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Username).IsRequired();
        e.HasIndex(x => x.Username).IsUnique();
        e.HasIndex(x => x.ExternalId).IsUnique(false);
        e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
    }
}

public class PlaybackSessionConfig : IEntityTypeConfiguration<PlaybackSession>
{
    public void Configure(EntityTypeBuilder<PlaybackSession> e)
    {
        e.HasKey(x => x.Id);
        e.HasOne(x => x.User).WithMany(x => x.Sessions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(x => x.MediaItem).WithMany().HasForeignKey(x => x.MediaItemId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(x => x.StreamRendition).WithMany().HasForeignKey(x => x.StreamRenditionId).OnDelete(DeleteBehavior.SetNull);
        e.Property(x => x.PlaybackType).HasConversion<int>();
        e.Property(x => x.PositionSeconds).HasDefaultValue(0);
        e.Property(x => x.StartedAtUtc).HasDefaultValueSql("NOW()");
        e.Property(x => x.LastUpdatedAtUtc).HasDefaultValueSql("NOW()");
        e.HasIndex(x => new { x.UserId, x.MediaItemId });
    }
}
