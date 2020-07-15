using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StarWars.Api.Controllers
{
    public class Character
    {
        public string name { get; set; }
		public IEnumerable<string> episodes { get; set; }
		public IEnumerable<string> friends { get; set; }
		public string? planet { get; set; }
    }

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
					name = "Luke Skywalker",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Han Solo", "Leia Organa", "C-3PO", "R2-D2" }
				},
				new Character
				{
					name = "Darth Vader",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Wilhuff Tarkin" }
				},
				new Character
				{
					name = "Han Solo",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Luke Skywalker", "Leia Organa", "R2-D2" }
				},
				new Character
				{
					name = "Leia Organa",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Luke Skywalker", "Han Solo", "C-3PO", "R2-D2" },
					planet = "Alderaan"
				},
				new Character
				{
					name = "Wilhuff Tarkin",
					episodes = new[] { "NEWHOPE" },
					friends = new[] { "Darth Vader" }
				},
				new Character
				{
					name = "C-3PO",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Luke Skywalker", "Han Solo", "Leia Organa", "R2-D2" }
				},
				new Character
				{
					name = "R2-D2",
					episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
					friends = new[] { "Luke Skywalker", "Han Solo", "Leia Organa" }
				}
			};

			return characters;
		}
    }
}
