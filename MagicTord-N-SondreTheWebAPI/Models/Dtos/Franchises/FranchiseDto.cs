using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises
{
    public class FranchiseDto
    {
        public int FranchiseID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual List<Movie> Movies { get; set; } = null!;
    }
}
