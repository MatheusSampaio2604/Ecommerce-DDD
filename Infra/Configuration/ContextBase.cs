using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<CompraUsuario> CompraUsuario { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<LogSistema> LogSistema { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }


        private static string GetStringConectionConfig()
        {
            return "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EcommerceDDD;Integrated Security=True";
        }


    }
}
