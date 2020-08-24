using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Interfaces
{
    public interface ICounterRepository<T> : IRepository<T> where T : class
    {
        Task<T> Update(int id);
    }
}