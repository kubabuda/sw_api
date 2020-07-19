using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class SwEpisode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<SwEpisodeCharacter> Characters { get; set; }
    }
}
