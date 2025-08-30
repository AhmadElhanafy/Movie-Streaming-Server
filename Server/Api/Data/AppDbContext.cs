// Data/AppDbContext.cs
using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MediaItem> MediaItems => Set<MediaItem>();
    public DbSet<StreamRendition> StreamRenditions => Set<StreamRendition>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<MediaPerson> MediaPeople => Set<MediaPerson>();
    public DbSet<LibraryPath> LibraryPaths => Set<LibraryPath>();
    public DbSet<User> Users => Set<User>();
    public DbSet<PlaybackSession> PlaybackSessions => Set<PlaybackSession>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
