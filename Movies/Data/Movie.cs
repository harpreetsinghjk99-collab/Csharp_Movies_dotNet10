using System.ComponentModel.DataAnnotations;

namespace Movies.App.Data;

public class Movie
{
    public int MovieId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public int? DirectorId { get; set; }

    //DirectorId is nullable because a movie can temporarily exist without a known director
    public Person? Director { get; set; }

    public ICollection<MovieActor> MovieActors { get; set; }
        = new List<MovieActor>();
}