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
        IEnumerable<SwCharacter> GetCharacters(int pageNr);
        
        SwCharacter GetCharacter(string key);
        
        void CreateCharacter(SwCharacter newCharacter);
        
        void UpdateCharacter(string key, SwCharacter character);

        void DeleteCharacter(string key);
    }
}
