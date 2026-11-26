using System.ComponentModel.DataAnnotations;

namespace Movies.App.Data;

public class Nationality
{
    //EF core recoganises NationalityId as the primary key because of the naming convention
    public int NationalityId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    public ICollection<Person> People { get; set; }
        = new List<Person>();
}