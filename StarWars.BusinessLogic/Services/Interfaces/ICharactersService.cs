
using StarWars.BusinessLogic.Models;
using System.Collections.Generic;

namespace StarWars.BusinessLogic.Services.Interfaces
{
    public interface ICharactersService
    {
        IEnumerable<Character> GetCharacters();
    }
}
