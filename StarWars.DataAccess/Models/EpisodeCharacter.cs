namespace StarWars.DataAccess.Models
{
    public class EpisodeCharacter
    {
        public int CharacterId { get; set; }
        public virtual Character SwCharacter { get; set; }
        public int EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }
    }
}
