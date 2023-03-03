using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    /// <summary>
    /// Model that reflects what the Franchise table holds in the database. 
    /// This is also used to create table if the database does not exist to begin with.
    /// </summary>
    public class Franchise
    {

        /// <summary>
        /// Constructor for the Franchise class
        /// </summary>
        public Franchise()
        {
            Movies = new HashSet<Movie>();
        }

        [Key]
        public int FranchiseID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = null!;

        [Required, StringLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        public virtual ICollection<Movie> Movies { get; set; } = null!;
    }
}
