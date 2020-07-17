using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        ICharactersService _charactersService;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ICharactersService charactersService,
            ILogger<CharactersController> logger)
        {
            _charactersService = charactersService;
            _logger = logger;
        }

		[HttpGet]
		public IEnumerable<Character> Get()
		{
            var characters = _charactersService.GetCharacters();

			return characters;
		}
    }
}
