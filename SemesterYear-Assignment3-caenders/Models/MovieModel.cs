using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemesterYear_Assignment3_caenders.Models
{
    public class MovieModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Title { get; set; }
        public uint? Release_year { get; set; }
        public string? IMDB_url { get; set; }
        public string? Media_url { get; set; }
        public Genre_e? Genre { get; set; }

        public ICollection<ActorModel> Actors { get; set; }

        [NotMapped] //  This is just for getting the full actors list to the details page
        public List<ActorModel> AvailableActors { get; set; }
    }

    public enum Genre_e
    {
        Action, Comedy, Horror, SciFi, Drama, Romance
    };
}
