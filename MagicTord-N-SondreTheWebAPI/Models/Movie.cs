using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    /// <summary>
    /// Model that reflects what the character table holds in the database. 
    /// This is also used to create table if the database does not exist to begin with
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Constructor for the Movie class
        /// </summary>
        public Movie()
        {
            Characters = new HashSet<Character>();
        }

        [Key]
        public int MovieID { get; set; }

        [Required, StringLength(50)]
        public string MovieTitle { get; set; } = null!;

        [Required, StringLength(30)]
        public string Genre { get; set; } = null!;

        [Required, StringLength(6)]
        public string ReleaseYear { get; set; } = null!;

        [Required, StringLength(50)]
        public string Director { get; set; } = null!;

        [Required, StringLength(300)]
        public string PictureURL { get; set; } = null!;

        [Required, StringLength(300)]
        public string TrailerURL { get; set; } = null!;

        [Required]
        public int FranchiseID { get; set; }

        [Required]
        public virtual ICollection<Character> Characters { get; set; } = null!;
    }
}
