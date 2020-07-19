namespace StarWars.DataAccess.Models
{
    public class SwEpisodeCharacter
    {
        public int SwCharacterId { get; set; }
        public virtual SwCharacter SwCharacter { get; set; }
        public int SwEpisodeId { get; set; }
        public virtual SwEpisode SwEpisode { get; set; }
    }
}
