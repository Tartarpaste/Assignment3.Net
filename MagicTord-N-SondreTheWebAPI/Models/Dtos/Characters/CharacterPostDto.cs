namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters
{
    /// <summary>
    ///  DTO used for adding a Character to the database.
    /// </summary>
    public class CharacterPostDto
    {
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
    }
}
