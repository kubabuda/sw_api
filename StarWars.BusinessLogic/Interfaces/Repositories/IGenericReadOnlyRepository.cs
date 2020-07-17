using System.Linq;

namespace StarWars.BusinessLogic.Interfaces.Repositories
{
    public interface IGenericReadOnlyRepository<T>
    {
        IQueryable<T> GetQueryable();
    }
}
