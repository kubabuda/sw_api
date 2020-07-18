using StarWars.BusinessLogic.Models;

namespace StarWars.BusinessLogic.Interfaces.Repositories
{
    public interface ICharacterRepository: IGenericReadOnlyRepository<Character>
    {
        void Create(Character character);
        void Update(string name, Character character);
        void Delete(string name);
    }
}
