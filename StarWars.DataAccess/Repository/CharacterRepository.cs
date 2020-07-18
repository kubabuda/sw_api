using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.DataAccess.Repository
{
	public class CharacterRepository : ICharacterRepository
    {
		private readonly static List<Character> s_characters = DefaultCharacters.Get();

        public IQueryable<Character> GetQueryable()
        {
			return s_characters.AsQueryable();
        }

		public void Create(Character character)
		{
			s_characters.Add(character);
		}

		public void Update(string name, Character character)
        {
            // handle not found..
            s_characters[FindIndex(name)] = character;
        }

        public void Delete(string name)
		{
			s_characters.RemoveAt(FindIndex(name));
		}

		private int FindIndex(string name)
		{
			// TODO refactor using ORM
			var characterToUpdate = s_characters.Where(c => c.Name == name).Single();
			var indexOf = s_characters.IndexOf(characterToUpdate);
			return indexOf;
		}

		
    }
}
