using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheAPI.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required, StringLength(50)]
        public string MovieTitle { get; set; }

        [Required, StringLength(30)]
        public string Genre { get; set; }

        [Required, StringLength(5)]
        public string ReleaseYear { get; set; }

        [Required, StringLength(50)]
        public string Director { get; set; }

        [Required, StringLength(300)]
        public string PictureURL { get; set; }

        [Required, StringLength(300)]
        public string TrailerURL { get; set; }

        [Required]
        public int FranchiseID { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
            
            
            
            

    }
}
