using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Interfaces
{
    public interface IReferenceInfoService<T> : IService<T> where T : class
    {
        Task<T> Find(string url, bool isLongReference = true);
        Task<List<T>> GetAll();
        Task Remove(int id);
    }
}