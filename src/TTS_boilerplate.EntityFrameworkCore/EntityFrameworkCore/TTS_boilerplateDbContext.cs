using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TTS_boilerplate.Authorization.Roles;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.MultiTenancy;
using TTS_boilerplate.Models;
using TTS_boilerplate.Core;
//using System.Threading.Tasks;

namespace TTS_boilerplate.EntityFrameworkCore
{
    public class TTS_boilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, TTS_boilerplateDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<CustomerInformation> CustomerInformation { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountOfUser> DiscountOfUsers { get; set; }

        public TTS_boilerplateDbContext(DbContextOptions<TTS_boilerplateDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<CartItem>()
             .Property(o => o.Status)
             .HasConversion<string>();

      modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne()
                .HasForeignKey(cItem => cItem.CartId);

            modelBuilder.Entity<Cart>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
         
            modelBuilder.Entity<CartItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(cItem => cItem.ProductId);
        }
    }
}

