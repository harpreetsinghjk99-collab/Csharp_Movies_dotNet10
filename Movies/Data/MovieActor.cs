namespace Movies.App.Data;

public class MovieActor
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int PersonId { get; set; }

    public Person Person { get; set; } = null!;
}

//movie can have many actors
//and one person can act in many movies.
//MovieActor resolves this many-to-many relationship
//by storing pairs of MovieId and PersonId.