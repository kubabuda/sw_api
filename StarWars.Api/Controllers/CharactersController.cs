using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersService _charactersService;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ICharactersService charactersService,
            ILogger<CharactersController> logger)
        {
            _charactersService = charactersService;
            _logger = logger;
        }

		[HttpGet]
        [SwaggerResponse(200, "The list of countries", typeof(IEnumerable<Character>))]
        public IEnumerable<Character> Get()
		{
            var characters = _charactersService.GetCharacters();

			return characters;
		}
    }
}
