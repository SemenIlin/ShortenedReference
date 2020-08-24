using ShortenedReferenceDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Interfaces
{
    public interface IReferenceInfoRepository
    {
        Task<ReferenceInfo> Find(string url, bool isLongReference = true);
        Task<ReferenceInfo> Get(int id);
        Task<List<ReferenceInfo>> GetAll();

        Task Remove(int id);
        Task Update(int id);
        Task<ReferenceInfo> Create(ReferenceInfo item);
    }
}