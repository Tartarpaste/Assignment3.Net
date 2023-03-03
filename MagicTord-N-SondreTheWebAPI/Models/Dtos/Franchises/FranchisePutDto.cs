namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises
{
    public class FranchisePutDto
    {
        /// <summary>
        /// DTO for updating a franchise in the database
        /// </summary>
        public int FranchiseID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
