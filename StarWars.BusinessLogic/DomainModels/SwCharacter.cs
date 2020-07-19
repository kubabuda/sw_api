using System.Collections.Generic;

namespace StarWars.BusinessLogic.Models
{
    public class SwCharacter
    {
        public string Name { get; set; }
        public IEnumerable<string> Episodes { get; set; }
        public IEnumerable<string> Friends { get; set; }
        public string? Planet { get; set; }
    }
}
