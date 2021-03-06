﻿using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.Services
{
    public class CharactersService : ICharactersService
    {
		private readonly IStarWarsApiConfiguration _configuration;
		private readonly ICharacterRepository _repository;
        private readonly IValidateActionsService _validator;

		public CharactersService(
            IStarWarsApiConfiguration configuration, 
			ICharacterRepository repository,
            IValidateActionsService validator)
        {
			_configuration = configuration;
            _repository = repository;
            _validator = validator;
        }

        public void CreateCharacter(SwCharacter newCharacter)
        {
            _repository.Create(newCharacter);
        }

        public IEnumerable<SwCharacter> GetCharacters(int pageNr)
        {
			return _repository.GetQueryable()
				.Skip((pageNr - 1) * _configuration.PageSize)
				.Take(_configuration.PageSize);
        }

        public SwCharacter GetCharacter(string name)
        {
            return _repository.GetQueryable()
                .Where(u => string.Equals(u.Name, name))
                .Single();
        }

        public void UpdateCharacter(string name, SwCharacter character)
        {
            ValidateUpdate(name, character);
            _repository.Update(name, character);
        }

        private void ValidateUpdate(string name, SwCharacter character)
        {
            if (!_validator.IsValidUpdate(name, character))
            {
                throw new InvalidOperationException();
            }
        }

        public void DeleteCharacter(string name)
        {
            _repository.Delete(name);
        }
    }
}
