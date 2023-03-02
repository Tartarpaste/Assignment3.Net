namespace MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters
{
    public class CharacterPutDto
    {
        public int CharacterID { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
    }
}
