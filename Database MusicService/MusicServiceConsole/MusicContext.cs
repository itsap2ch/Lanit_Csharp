using Microsoft.EntityFrameworkCore;

namespace MusicServiceConsole.Data;

public class MusicContext : DbContext
{
    public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().ToTable("Artists");
    }
}