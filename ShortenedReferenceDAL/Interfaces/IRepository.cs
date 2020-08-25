using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task<T> Get(int id);
        Task<T> Create(T item);
    }
}
