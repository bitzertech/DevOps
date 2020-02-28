using Microsoft.EntityFrameworkCore;
using MyMoviesMvc.DataModels;

namespace MyMoviesMvc.Context
{

    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        { }

        //entities
        public DbSet<MovieModel> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieModel>().HasData(
                new MovieModel { Id = -10, Title = "The Shawshank redemption", Year = 1994, Director = "Frank Darabont" },
                new MovieModel { Id = -9, Title = "The Godfather", Year = 1972, Director = "Francis Ford Coppola" },
                new MovieModel { Id = -8, Title = "The Dark Knight", Year = 2008, Director = "Christopher Nolan" },
                new MovieModel { Id = -7, Title = "The Godfather: Part II", Year = 1974, Director = "Francis Ford Coppola" },
                new MovieModel { Id = -6, Title = "The Lord of the Rings: The Return of the King", Year = 2003, Director = "Peter Jackson" },
                new MovieModel { Id = -5, Title = "Pulp Fiction", Year = 1994, Director = "Quentin Tarantino" },
                new MovieModel { Id = -4, Title = "Schindler's List", Year = 1993, Director = "Steven Spielberg" },
                new MovieModel { Id = -3, Title = "12 Angry Men", Year = 1957, Director = "Sidney Lumet" },
                new MovieModel { Id = -2, Title = "Inception", Year = 2010, Director = "Christopher Nolan" },
                new MovieModel { Id = -1, Title = "Fight Club", Year = 1999, Director = "David Fincher" }
            );
        }
    }
}