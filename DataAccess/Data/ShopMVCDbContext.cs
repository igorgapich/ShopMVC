using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ShopMVCDbContext : IdentityDbContext
    {

        //public ShopMVCDbContext()
        //{
        //    //Database.EnsureDeleted();
        //    //Database.EnsureCreated();
        //}
        public ShopMVCDbContext(DbContextOptions<ShopMVCDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopMVC;Integrated Security=True;"); 
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(SeedData.GetCategory());
            modelBuilder.Entity<Product>().HasData(SeedData.GetProduct());

            #region Fluemt API => Configuration
            //// Set Primary Key
            //modelBuilder.Entity<Product>()
            //    .HasKey(x => x.Id);

            //// Set property configuration
            //modelBuilder.Entity<Product>()
            //    .Property(x => x.Name)
            //    .HasMaxLength(150)
            //    .IsRequired();

            //// Set relationship configuration

            //modelBuilder.Entity<Product>()
            //    .HasOne(x => x.Category)
            //    .WithMany(x => x.Products)
            //    .HasForeignKey(x => x.CategoryId);
            #endregion

            //ApplyConfigurations for Entities
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopMVCDbContext).Assembly);
            //or
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}