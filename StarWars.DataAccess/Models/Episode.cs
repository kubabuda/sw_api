using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<EpisodeCharacter> Characters { get; set; }
    }
}
