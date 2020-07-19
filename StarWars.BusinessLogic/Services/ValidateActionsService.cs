using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services.Interfaces;

namespace StarWars.BusinessLogic.Services
{
    public class ValidateActionsService : IValidateActionsService
    {
        public bool IsValidUpdate(string name, Character character)
        {
            return string.Equals(name, character.Name);
        }
    }
}
