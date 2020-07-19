﻿using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.DataAccess.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly static List<SwCharacter> s_characters = DefaultCharacters.Get();
        private readonly StarWarsDbContext _dbContext;

        public CharacterRepository(StarWarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<SwCharacter> GetQueryable()
        {
            return s_characters.AsQueryable();
        }

        public void Create(SwCharacter character)
        {
            s_characters.Add(character);
        }

        public void Update(string name, SwCharacter character)
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
