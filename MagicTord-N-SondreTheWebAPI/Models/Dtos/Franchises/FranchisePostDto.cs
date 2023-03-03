using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises
{
    /// <summary>
    /// DTO used for adding a Franchise to the database.
    /// </summary>
    public class FranchisePostDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
