using StarWars.BusinessLogic.Models;
using System.Collections.Generic;

namespace StarWars.Api.ApiModels
{
    public class CharacterResponse
    {
        public IEnumerable<SwCharacter> Characters { get; set; }
    }
}
