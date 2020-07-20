using StarWars.DataAccess.Models;
using System.Collections.Generic;

namespace StarWars.Api.ApiModels
{
    public class CharacterResponse
    {
        public IEnumerable<Character> Characters { get; set; }
    }
}
