using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises
{
    /// <summary>
    /// We don't need to have any other dto's for put because we use the same values
    /// </summary>
    public class FranchiseDto
    {
        public int FranchiseID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual List<Movie> Movies { get; set; } = null!;
    }
}
