using StarWars.BusinessLogic.Models;

namespace StarWars.BusinessLogic.Services.Interfaces
{
    public interface IValidateActionsService
    {
        bool IsValidUpdate(string name, SwCharacter character);
    }
}
