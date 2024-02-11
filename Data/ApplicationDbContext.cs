using Microsoft.EntityFrameworkCore;
using SoftLineTestApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> builder) : base(builder)
        {
            Database.EnsureCreated();
            
            //buider.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SoftLineDb;Trusted_Connection=True;");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(
                new Status { Id = 1, StatusName = "Создана" },
                new Status { Id = 2, StatusName = "В работе" },
                new Status { Id = 3, StatusName = "Завершена" }
            );

            builder.Entity<Goal>().HasOne<Status>("Status");
        }
    }
}
