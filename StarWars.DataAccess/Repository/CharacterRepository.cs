using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.DataAccess.Repository
{
	public class CharacterRepository : ICharacterRepository
    {
		private readonly static List<Character> s_characters = Initialize();

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

		private static List<Character> Initialize() => new List<Character>
		{
			new Character
			{
				Name = "Luke Skywalker",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Han Solo", "Leia Organa", "C-3PO", "R2-D2" }
			},
			new Character
			{
				Name = "Darth Vader",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Wilhuff Tarkin" }
			},
			new Character
			{
				Name = "Han Solo",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Luke Skywalker", "Leia Organa", "R2-D2" }
			},
			new Character
			{
				Name = "Leia Organa",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Luke Skywalker", "Han Solo", "C-3PO", "R2-D2" },
				Planet = "Alderaan"
			},
			new Character
			{
				Name = "Wilhuff Tarkin",
				Episodes = new[] { "NEWHOPE" },
				Friends = new[] { "Darth Vader" }
			},
			new Character
			{
				Name = "C-3PO",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Luke Skywalker", "Han Solo", "Leia Organa", "R2-D2" }
			},
			new Character
			{
				Name = "R2-D2",
				Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
				Friends = new[] { "Luke Skywalker", "Han Solo", "Leia Organa" }
			}
		};
    }
}
