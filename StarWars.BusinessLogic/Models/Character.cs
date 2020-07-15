using System.Collections.Generic;

namespace StarWars.BusinessLogic.Models
{
    public class Character
    {
        public string name { get; set; }
        public IEnumerable<string> episodes { get; set; }
        public IEnumerable<string> friends { get; set; }
        public string? planet { get; set; }
    }
}
