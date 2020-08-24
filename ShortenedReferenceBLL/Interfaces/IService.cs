using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Interfaces
{
    public interface IService<T>
    {
        Task<T> Create(T item);
        Task<T> Get(int id);
    }
}