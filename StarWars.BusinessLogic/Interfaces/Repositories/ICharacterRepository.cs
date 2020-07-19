using StarWars.BusinessLogic.Models;

namespace StarWars.BusinessLogic.Interfaces.Repositories
{
    public interface ICharacterRepository: IGenericReadOnlyRepository<SwCharacter>
    {
        void Create(SwCharacter character);
        void Update(string name, SwCharacter character);
        void Delete(string name);
    }
}
