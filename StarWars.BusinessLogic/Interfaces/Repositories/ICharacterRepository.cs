using StarWars.BusinessLogic.Models;

namespace StarWars.BusinessLogic.Interfaces.Repositories
{
    public interface ICharacterRepository: IGenericReadOnlyRepository<Character>
    {
        void Create(Character character);
    }
}
