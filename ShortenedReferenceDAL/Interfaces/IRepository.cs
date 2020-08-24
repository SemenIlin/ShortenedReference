using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T item);
        Task<T> Get(int id);
    }
}