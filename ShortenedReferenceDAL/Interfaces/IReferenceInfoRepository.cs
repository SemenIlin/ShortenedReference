using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Interfaces
{
    public interface IReferenceInfoRepository<T> : IRepository<T> where T :class
    {
        Task<T> Find(string url, bool isLongReference = true);
        Task<List<T>> GetAll();
        Task Remove(int id);
        Task Update(int id);
    }
}