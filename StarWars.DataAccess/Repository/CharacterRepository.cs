using AutoMapper;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.DataAccess.Models;
using System.Linq;

namespace StarWars.DataAccess.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarWarsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CharacterRepository(StarWarsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<SwCharacter> GetQueryable()
        {
            return _mapper.ProjectTo<SwCharacter>(_dbContext.Characters);
        }

        public void Create(SwCharacter character)
        {
            var newEntity = _mapper.Map<Character>(character);
            // TODO take care to add also episodes and friends
            _dbContext.Characters.Add(newEntity); 
            _dbContext.SaveChanges(); // TODO how about async API
        }

        public void Update(string name, SwCharacter character)
        {
            // handle not found..
            //s_characters[FindIndex(name)] = character;
        }

        public void Delete(string name)
        {
            var toRemove = _dbContext.Characters.Where(c => c.Name == name).Single();
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }
    }
}
