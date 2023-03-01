using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models
{
    public class Franchise
    {

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
