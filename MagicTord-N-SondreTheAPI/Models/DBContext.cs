using MagicTord_N_SondreTheAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTord_N_SondreTheAPI.Models
{
    public class DBContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringHelper.getConnectionString());
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(x => x.MovieID);
            modelBuilder.Entity<Movie>()
                .HasMany(b => b.Characters)
                .WithMany(b => b.Movies);

            modelBuilder.Entity<Franchise>().HasKey(x => x.FranchiseID);

            modelBuilder.Entity<Franchise>().HasMany(b => b.Movies);
            modelBuilder.Entity<Character>().HasKey(x => x.CharacterID);
        }






    }
}
