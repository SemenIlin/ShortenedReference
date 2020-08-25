using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<T> Create(T item);
        Task<T> Get(int id);
    }
}
