using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models.MVVM
{
    public class AppDbContext : DbContext
    {
        // DbContextOptions'ı kabul eden constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:ElbruzWebPj"]);

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vw_MyOrders> Vw_MyOrders { get; set; }


    }
}
