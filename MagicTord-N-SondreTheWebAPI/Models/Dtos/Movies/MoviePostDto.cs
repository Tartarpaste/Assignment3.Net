namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies
{
    /// <summary>
    /// DTO used for adding a Movie to the database.
    /// </summary>
    public class MoviePostDto
    {
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseYear { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public string TrailerURL { get; set; } = null!;
        public int FranchiseID { get; set; }
    }
}
