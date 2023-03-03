namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies
{

    /// <summary>
    /// The DTO for Movie, contains all related data.
    /// </summary>
    public class MovieDto
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseYear { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public string TrailerURL { get; set; } = null!;
        public int FranchiseID { get; set; } 
        public virtual List<Character> Characters { get; set; } = null!;
    }
}
