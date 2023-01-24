using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Data
{
    public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure specific rules for entities
            modelBuilder.Entity<Charge>(entity =>
            {
                entity.Property(x => x.Type).HasConversion<string>();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(x => x.InvoiceNumber)
                      .IsUnique();

                entity.Property(x => x.Tax).HasDefaultValue(0);
                entity.Property(x => x.Discount).HasDefaultValue(0);

                entity.HasMany(x => x.InvoiceDetails)
                        .WithOne(x => x.Invoice)
                        .HasForeignKey(x => x.InvoiceId);
            });


            modelBuilder.Entity<InvoiceCharge>(entity =>
                {
                    entity.HasKey(x => new { x.InvoiceId, x.ChargeId });

                    entity.HasOne(x => x.Charge)
                            .WithMany()
                            .HasForeignKey(x => x.ChargeId);

                    entity.HasOne(x => x.Invoice)
                            .WithMany()
                            .HasForeignKey(x => x.InvoiceId);
                });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Items)
                        .WithOne(x => x.Menu)
                        .HasForeignKey(x => x.MenuId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(x => x.Status)
                            .HasConversion<string>();

                entity.HasOne(x => x.Table);

                entity.HasOne(x => x.Waiter);

                entity.HasOne(x => x.Cheff);
                        
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(x => x.MenuItem);
            });


            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.MenuItem);
            });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
