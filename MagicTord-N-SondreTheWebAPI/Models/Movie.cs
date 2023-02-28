using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    public class Movie
    {

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

        [Required, StringLength(5)]
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
