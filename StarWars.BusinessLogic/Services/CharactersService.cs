using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace StarWars.BusinessLogic.Services
{
    public class CharactersService : ICharactersService
    {
        public IEnumerable<Character> GetCharacters()
        {
            throw new NotImplementedException();
        }
    }
}
