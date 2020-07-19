using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<EpisodeCharacter> EpisodeCharacters { get; set; }
    }
}
