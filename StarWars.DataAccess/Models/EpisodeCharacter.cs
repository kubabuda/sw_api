namespace StarWars.DataAccess.Models
{
    public class EpisodeCharacter
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}
