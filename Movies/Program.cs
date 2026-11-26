using Microsoft.EntityFrameworkCore;
using Movies.App.Data;

using var db = new MoviesContext();

PrintHeading("DATABASE INFORMATION");

Console.WriteLine($"Location: {db.DbPath}");
Console.WriteLine($"Connected: {db.Database.CanConnect()}");


PrintHeading("1. NATIONALITIES");

var nationalities = db.Nationalities
    .OrderBy(n => n.NationalityId)
    .ToList();

foreach (var nationality in nationalities)
{
    Console.WriteLine(
        $"ID: {nationality.NationalityId}, " +
        $"Title: {nationality.Title}");
}


PrintHeading("2. PEOPLE AND THEIR NATIONALITIES");

var people = db.People
    .Include(p => p.Nationality)
    .OrderBy(p => p.LastName)
    .ToList();

foreach (var person in people)
{
    Console.WriteLine(
        $"{person.PersonId}: " +
        $"{person.FirstName} {person.LastName} | " +
        $"Born: {person.Birthday:dd MMMM yyyy} | " +
        $"Nationality: {person.Nationality.Title}");
}


PrintHeading("3. MOVIES AND DIRECTORS");

var moviesWithDirectors = db.Movies
    .Include(m => m.Director)
    .OrderBy(m => m.Title)
    .ToList();

foreach (var movie in moviesWithDirectors)
{
    string director = movie.Director is null
        ? "Unknown"
        : $"{movie.Director.FirstName} {movie.Director.LastName}";

    Console.WriteLine(
        $"{movie.MovieId}: {movie.Title} " +
        $"({movie.ReleaseDate:yyyy})");

    Console.WriteLine($"   Director: {director}");
}


PrintHeading("4. MOVIES AND ACTORS");

var moviesWithActors = db.Movies
    .Include(m => m.MovieActors)
    .ThenInclude(ma => ma.Person)
    .OrderBy(m => m.Title)
    .ToList();

foreach (var movie in moviesWithActors)
{
    Console.WriteLine(movie.Title);

    if (movie.MovieActors.Count == 0)
    {
        Console.WriteLine("   No actors recorded.");
    }
    else
    {
        foreach (var movieActor in movie.MovieActors)
        {
            Console.WriteLine(
                $"   Actor: {movieActor.Person.FirstName} " +
                $"{movieActor.Person.LastName}");
        }
    }
}


PrintHeading("5. FILTERING: BRITISH PEOPLE");

var britishPeople = db.People
    .Include(p => p.Nationality)
    .Where(p => p.Nationality.Title == "British")
    .OrderBy(p => p.LastName)
    .ToList();

foreach (var person in britishPeople)
{
    Console.WriteLine(
        $"{person.FirstName} {person.LastName}");
}


PrintHeading("6. SUMMARY");

Console.WriteLine($"Number of nationalities: {db.Nationalities.Count()}");
Console.WriteLine($"Number of people:        {db.People.Count()}");
Console.WriteLine($"Number of movies:        {db.Movies.Count()}");
Console.WriteLine($"Movie–actor connections: {db.MovieActors.Count()}");


PrintHeading("DEMONSTRATION COMPLETE");

Console.WriteLine("Press any key to close.");
Console.ReadKey();


static void PrintHeading(string heading)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 60));
    Console.WriteLine(heading);
    Console.WriteLine(new string('=', 60));
}