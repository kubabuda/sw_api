﻿using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.Services
{
    public class CharactersService : ICharactersService
    {
		private readonly IStarWarsApiConfiguration _configuration;
		private readonly ICharacterRepository _repository;

		public CharactersService(IStarWarsApiConfiguration configuration, 
			ICharacterRepository repository)
        {
			_configuration = configuration;
            _repository = repository;
        }

        public void CreateCharacter(Character newCharacter)
        {
            _repository.Create(newCharacter);
        }

        public IEnumerable<Character> GetCharacters(int pageNr)
        {
			return _repository.GetQueryable()
				.Skip((pageNr - 1) * _configuration.PageSize)
				.Take(_configuration.PageSize);
        }

        public Character GetCharacter(string name)
        {
            return _repository.GetQueryable()
                .Where(u => string.Equals(u.Name, name))
                .Single();
        }
    }
}
