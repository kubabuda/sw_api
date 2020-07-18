using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Web;

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
        [SwaggerResponse(200, "The list of characters", typeof(IEnumerable<Character>))]
        public IEnumerable<Character> GetAll([FromQuery] int pageNr = 1)
		{
            var characters = _charactersService.GetCharacters(pageNr);

			return characters;
		}

        [HttpGet("{key}")]
        [SwaggerResponse(200, "Character details", typeof(Character))]
        [SwaggerResponse(404, "Character not found", typeof(Character))]
        public ActionResult Get([FromRoute] string key)
        {
            var name = Decode(key);
            try
            {
                return base.Ok(_charactersService.GetCharacter(name));
            }
            catch (InvalidOperationException)
            {
                _logger.LogError(@$"Failed to find user '{name}'");

                return NotFound();
            }
        }

        [HttpPost]
        [SwaggerResponse(204, "No content", typeof(void))]
        public void Post([FromBody] Character newCharacter)
        {
            _charactersService.CreateCharacter(newCharacter);
        }

        [HttpPut("{key}")]
        [SwaggerResponse(204, "No content", typeof(void))]
        public void Put([FromRoute] string key, [FromBody] Character character)
        {
            var name = Decode(key);
            _charactersService.UpdateCharacter(name, character);
        }

        private string Decode(string key)
        {
            return HttpUtility.UrlDecode(key);
        }
    }
}
