namespace MagicTord_N_SondreTheWebAPI.Models
{
    /// <summary>
    /// Model that reflects what the CharacterMovie table holds in the database. 
    /// This is also used to create table if the database does not exist to begin with
    /// </summary>
    public class CharacterMovie
    {
        public int MovieID { get; set; }
        public int CharacterID { get; set; }
    }
}
