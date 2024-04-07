
using RealTime_StockExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RealTime_StockExchange.Models;

namespace StockUpdates.Context

    {
        public partial class StockUpdatesContext : DbContext 
        {
            public StockUpdatesContext()
            {
            }

            public StockUpdatesContext(DbContextOptions<StockUpdatesContext> options)
                : base(options)
            {
            }

            public virtual DbSet<Stock> Stocks { get; set; }
            public virtual DbSet<Order> Orders { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=Hady-Sharawi\\SQLEXPRESS;Database=ClientStock;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
            

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Stocks");

                entity.Property(e => e.c).HasColumnName("close_price");

                entity.Property(e => e.h).HasColumnName("highest_price");

                entity.Property(e => e.l).HasColumnName("lowest_price");

            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);   

                entity.ToTable("Orders");


            });





            OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
    }




