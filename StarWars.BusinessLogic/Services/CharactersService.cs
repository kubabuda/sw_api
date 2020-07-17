using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.Services
{
    public class CharactersService : ICharactersService
    {
		private readonly IStarWarsApiConfiguration _configuration;

		public CharactersService(IStarWarsApiConfiguration configuration)
        {
			_configuration = configuration;
        }

		public IEnumerable<Character> GetCharacters(int pageNr)
        {
			return _characters.AsQueryable()
				.Skip((pageNr - 1) * _configuration.PageSize)
				.Take(_configuration.PageSize);
        }

        private static readonly IEnumerable<Character> _characters = new List<Character>
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
