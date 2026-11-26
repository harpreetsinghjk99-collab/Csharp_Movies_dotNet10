using Microsoft.EntityFrameworkCore;

namespace Movies.App.Data;

public class MoviesContext : DbContext
{
    public string DbPath { get; }

    public MoviesContext()
    {
        string documents =
            Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments);

        DbPath = Path.Combine(documents, "movies-net10.db");
    }

    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<Person> People => Set<Person>();

    public DbSet<Nationality> Nationalities =>
        Set<Nationality>();

    public DbSet<MovieActor> MovieActors =>
        Set<MovieActor>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            $"Data Source={DbPath}");
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .HasOne(movie => movie.Director)
            .WithMany()
            .HasForeignKey(movie => movie.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<MovieActor>()
            .HasKey(movieActor => new
            {
                movieActor.MovieId,
                movieActor.PersonId
            });

        modelBuilder.Entity<MovieActor>()
            .HasOne(movieActor => movieActor.Movie)
            .WithMany(movie => movie.MovieActors)
            .HasForeignKey(movieActor => movieActor.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(movieActor => movieActor.Person)
            .WithMany(person => person.MovieActors)
            .HasForeignKey(movieActor => movieActor.PersonId);

        modelBuilder.Entity<Nationality>().HasData(
            new Nationality
            {
                NationalityId = 1,
                Title = "British"
            },
            new Nationality
            {
                NationalityId = 2,
                Title = "French"
            },
            new Nationality
            {
                NationalityId = 3,
                Title = "American"
            });

        modelBuilder.Entity<Person>().HasData(
            new Person
            {
                PersonId = 1,
                FirstName = "Simon",
                LastName = "Pegg",
                Birthday = new DateTime(1970, 2, 14),
                NationalityId = 1
            },
            new Person
            {
                PersonId = 2,
                FirstName = "Benedict",
                LastName = "Cumberbatch",
                Birthday = new DateTime(1976, 7, 19),
                NationalityId = 1
            },
            new Person
            {
                PersonId = 3,
                FirstName = "Jean",
                LastName = "Reno",
                Birthday = new DateTime(1948, 7, 30),
                NationalityId = 2
            },
            new Person
            {
                PersonId = 4,
                FirstName = "Chris",
                LastName = "Pine",
                Birthday = new DateTime(1980, 8, 26),
                NationalityId = 3
            },
            new Person
            {
                PersonId = 5,
                FirstName = "J. J.",
                LastName = "Abrams",
                Birthday = new DateTime(1966, 6, 27),
                NationalityId = 3
            });

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                MovieId = 1,
                Title = "Star Wars: The Force Awakens",
                ReleaseDate = new DateTime(2015, 12, 18),
                DirectorId = 5
            },
            new Movie
            {
                MovieId = 2,
                Title = "Star Trek",
                ReleaseDate = new DateTime(2009, 5, 8),
                DirectorId = 5
            });

        modelBuilder.Entity<MovieActor>().HasData(
            new MovieActor
            {
                MovieId = 2,
                PersonId = 1
            },
            new MovieActor
            {
                MovieId = 2,
                PersonId = 4
            });
    }
}