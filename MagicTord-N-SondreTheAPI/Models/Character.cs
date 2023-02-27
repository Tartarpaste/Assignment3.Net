using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheAPI.Models
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }

        [Required, StringLength(50)]
        public string FullName { get; set; }

        [StringLength(40)]
        public string Alias { get; set; }

        [Required,StringLength(50)]
        public string Gender { get; set; }

        [Required,StringLength(300)]
        public string PictureURL { get; set; }

        public virtual ICollection<Movie> Movies { get;}
    }
}
