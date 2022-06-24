using Microsoft.EntityFrameworkCore;

namespace WarehouseManager.Entites
{
    public class WarehauseManagerDbContext : DbContext
    {
        private string _dbConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=WarehouseManagerDb;Trusted_Connection=True;";

        public DbSet<Company> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseCargo> WarehouseCargoes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
                .Property(r => r.Email)
                .IsRequired();

            // Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            // Company
            modelBuilder.Entity<Company>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Company>()
                .Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Company>()
                .Property(r => r.Description)
                .IsRequired();

            //Location
            modelBuilder.Entity<Location>()
                .Property(r => r.LocationName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Location>()
                .Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(50);

            //Warehouse
            modelBuilder.Entity<Warehouse>()
                .Property(r => r.WarehouseName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Warehouse>()
                .Property(r => r.StorageType)
                .IsRequired();

            modelBuilder.Entity<Warehouse>()
                .Property(r => r.MaximumCapacity)
                .IsRequired();

            modelBuilder.Entity<Warehouse>()
                .Property(r => r.CurrentCapacity)
                .IsRequired();

            //WarehouseCargo
            modelBuilder.Entity<WarehouseCargo>()
                .Property(r => r.CargoName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<WarehouseCargo>()
                .Property(r => r.CargoType)
                .IsRequired();

            modelBuilder.Entity<WarehouseCargo>()
                .Property(r => r.Volume)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString);
        }
    }
}
