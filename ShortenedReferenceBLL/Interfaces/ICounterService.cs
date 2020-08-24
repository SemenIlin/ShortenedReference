using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Interfaces
{
    public interface ICounterService<T> : IService<T> where T : class
    {
        Task<T> Update(int id);
    }
}