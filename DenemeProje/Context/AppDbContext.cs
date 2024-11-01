using DenemeProje.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DenemeProje.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("CustomerTable");
            modelBuilder.Entity<Customer>().HasKey("Id");
            modelBuilder.Entity<Customer>().Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<Customer>().Property(p => p.Name).HasColumnName("Name").IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Surname).HasColumnName("Surname").IsRequired(); 
            modelBuilder.Entity<Customer>().Property(p => p.Age).HasColumnName("Age").IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Mail).HasColumnName("Mail").IsRequired();
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
