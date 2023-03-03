using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    /// <summary>
    /// Model that reflects what the character table holds in the database. 
    /// This is also used to create table if the database does not exist to begin with
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Constructor for the Character class
        /// </summary>
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
