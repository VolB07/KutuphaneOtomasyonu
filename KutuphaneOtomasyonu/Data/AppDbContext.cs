using Microsoft.EntityFrameworkCore;


namespace KutuphaneOtomasyonu.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Buraya DbSet<T> tanımlarını ekleyebilirsin
        // public DbSet<KendiModelin> ModelAdi { get; set; }
    }
}
