using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    
    public class Character
    {

        public Character()
        {
            Movies = new HashSet<Movie>();
        }

        [Key]
        public int CharacterID { get; set; }

        [Required, StringLength(50)]
        public string FullName { get; set; } = null!;

        [StringLength(40)]
        public string Alias { get; set; } = null!;

        [Required,StringLength(50)]
        public string Gender { get; set; } = null!;

        [Required,StringLength(300)]
        public string PictureURL { get; set; } = null!;


        [Required]
        public virtual ICollection<Movie> Movies { get; set; } = null!;
    }
}
