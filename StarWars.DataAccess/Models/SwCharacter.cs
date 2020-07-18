using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class SwCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Planet { get; set; }
        
        public IEnumerable<SwEpisode> Episodes { get; set; }
        public IEnumerable<SwCharacter> Friends { get; set; }
    }
}
