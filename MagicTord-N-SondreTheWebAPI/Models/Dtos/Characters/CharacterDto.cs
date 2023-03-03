using System.ComponentModel.DataAnnotations;

namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters
{
    /// <summary>
    /// The DTO for Character, contains all related data.
    /// </summary>
    public class CharacterDto
    {
        public int CharacterID { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public virtual List<Movie> Movies { get; set; } = null!;
    }
}
