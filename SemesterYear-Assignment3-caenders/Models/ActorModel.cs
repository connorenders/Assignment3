using System.ComponentModel.DataAnnotations;

namespace SemesterYear_Assignment3_caenders.Models
{
    public class ActorModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public Gender_e? Gender { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? Media_URL { get; set; } = string.Empty;
        public string? IMDB_URL { get; set; } = string.Empty;

        public ICollection<MovieModel> Movies { get; set; }
    }

    public enum Gender_e
    {
        Male, Female, Other
    };
}
