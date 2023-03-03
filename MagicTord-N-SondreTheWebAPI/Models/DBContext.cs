using MagicTord_N_SondreTheWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    /// <summary>
    /// The comntext file of the project. Ensures the creation of the database has the correct relations between tables
    /// </summary>
    public class DBContext: DbContext
    {
        public DBContext() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringHelper.getConnectionString());
        }

        public virtual DbSet<Character> Characters { get; set; } = null!;
        public virtual DbSet<Franchise> Franchises { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<CharacterMovie> CharacterMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Structure and relationship creation
            modelBuilder.Entity<Movie>().HasKey(x => x.MovieID);

            modelBuilder.Entity<Franchise>().HasKey(x => x.FranchiseID);

            modelBuilder.Entity<Franchise>().HasMany(b => b.Movies);
            modelBuilder.Entity<Character>().HasKey(x => x.CharacterID);


            modelBuilder.Entity<Franchise>().HasData(
                new Franchise() { FranchiseID = 1, Name = "NCU", Description = "The Noroff Cinematic Universe" },
                new Franchise() { FranchiseID = 2, Name = "ZZZ", Description = "Zisson Zinematic Zeries" }
                );

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { MovieID = 1, MovieTitle = "Cocain Bear", Genre = "Action/Comedy", ReleaseYear = "2023", Director = "Nicholas Dean Nutz", PictureURL = "https://movies.universalpictures.com/media/01-cb-dm-mobile-banner-1080x745-pl-f01-112222-638e6de200084-1.jpg", TrailerURL = "https://www.youtube.com/watch?v=DuWEEKeJLMI", FranchiseID = 1 },
                new Movie() { MovieID = 2, MovieTitle = "Tetris", Genre = "Action/Comedy", ReleaseYear = "2023", Director = "Nicholas Dean Nutz", PictureURL = "https://static2.tribute.ca/poster/160x236/tetris-apple-tv-167535.jpg", TrailerURL = "https://www.youtube.com/watch?v=-BLM1naCfME", FranchiseID = 1 },
                new Movie() { MovieID = 3, MovieTitle = "Zathura", Genre = "Adventure", ReleaseYear = "2005", Director = "Zackary Zuckerberg", PictureURL = "https://www.themoviedb.org/t/p/original/gtlqXDVrF8E59Se2LKnBdbTw6oa.jpg", TrailerURL = "https://www.youtube.com/watch?v=zNxm_obDpNU", FranchiseID = 2 }
                );

            modelBuilder.Entity<Character>().HasData(
                new Character() { CharacterID = 1, FullName = "Boris", Alias = "The Cocaine Bear", Gender = "Female", PictureURL = "https://static01.nyt.com/images/2022/12/01/lens/01xp-cocainebear/01xp-cocainebear-mediumSquareAt3X.jpg" },
                new Character() { CharacterID = 2, FullName = "Carol Cocaine", Alias = "The Cocaine", Gender = "Female", PictureURL = "https://i2-prod.chroniclelive.co.uk/incoming/article18860908.ece/ALTERNATES/s615/1_snr_nec_010920dale_01.jpg" },
                new Character() { CharacterID = 3, FullName = "Eger Taronton", Alias = "The King", Gender = "Male", PictureURL = "https://i2-prod.chroniclelive.co.uk/incoming/article18860908.ece/ALTERNATES/s615/1_snr_nec_010920dale_01.jpg" },
                new Character() { CharacterID = 4, FullName = "Tharlize Cheron", Alias = "Furiosa", Gender = "Female", PictureURL = "https://www.indiewire.com/wp-content/uploads/2021/05/furiosa-movie.png?w=780" }
                );

            modelBuilder.Entity<Movie>()
            .HasMany(mov => mov.Characters)
            .WithMany(cha => cha.Movies)
            .UsingEntity<CharacterMovie>(
                mc => mc.HasOne<Character>().WithMany(),
                mc => mc.HasOne<Movie>().WithMany(),
                mc =>
                {
                    mc.ToTable("CharacterMovie");
                    mc.HasKey(mc => new { mc.MovieID, mc.CharacterID });
                    mc.HasData(
                        new CharacterMovie { MovieID = 1, CharacterID = 1 },
                        new CharacterMovie { MovieID = 1, CharacterID = 2 },
                        new CharacterMovie { MovieID = 2, CharacterID = 3 },
                        new CharacterMovie { MovieID = 2, CharacterID = 2 },
                        new CharacterMovie { MovieID = 3, CharacterID = 4 },
                        new CharacterMovie { MovieID = 3, CharacterID = 1 }
                    );
                });
        }
    }
}
