using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.BusinessLogic.Models;
using System.Collections.Generic;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ILogger<CharactersController> logger)
        {
            _logger = logger;
        }

		[HttpGet]
		public IEnumerable<Character> Get()
		{
			var characters = new List<Character>
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

			return characters;
		}
    }
}
