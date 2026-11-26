using System.ComponentModel.DataAnnotations;

namespace Movies.App.Data;

public class Person
{
    public int PersonId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }

    //NationalityId is a foreign key connecting a person to a nationality
    public int NationalityId { get; set; }

    public Nationality Nationality { get; set; } = null!;

    public ICollection<MovieActor> MovieActors { get; set; }
        = new List<MovieActor>();
}