using CocktailClient.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CocktailClient.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alcool>().HasData(
                new Alcool { Id = 1, Name = "Avec Alcool" },
                new Alcool { Id = 2, Name = "Sans Alcool" }
            );

            modelBuilder.Entity<SuperRecette>().HasData(
                new SuperRecette
                {
                    Id = 1,
                    Name = "ABC",
                    Ingredient = "Rhum",
                    Description = "boisson forte",
                    AlcoolId = 1,
                },
                new SuperRecette
                {
                    Id = 2,
                    Name = "Grenadine",
                    Ingredient = "eau, sirop",
                    Description = "à consommer à tout heure",
                    AlcoolId = 2
                }
            );
        }

        public DbSet<SuperRecette> SuperRecettes { get; set; }

        public DbSet<Alcool> Alcools { get; set; }
    }
}
