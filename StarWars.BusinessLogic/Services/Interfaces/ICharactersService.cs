using StarWars.BusinessLogic.Models;
using System.Collections.Generic;

namespace StarWars.BusinessLogic.Services.Interfaces
{
    public interface ICharactersService
    {
        /// <summary>
        /// Get movie characters from storage
        /// </summary>
        /// <param name="pageNr">Nr of page in pagination. Counting like humans, not programmers: pages 1, 2, 3...</param>
        /// <returns></returns>
        IEnumerable<Character> GetCharacters(int pageNr);
        void CreateCharacter(Character newCharacter);
    }
}
