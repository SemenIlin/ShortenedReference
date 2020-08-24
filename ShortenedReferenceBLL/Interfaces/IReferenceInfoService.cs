using ShortenedReferenceBLL.ModelDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Interfaces
{
    public interface IReferenceInfoService
    {
        Task<ReferenceInfoDto> Find(string url, bool isLongReference = true);
        Task<ReferenceInfoDto> Get(int id);
        Task<List<ReferenceInfoDto>> GetAll();

        Task Remove(int id);
        Task Update(int id);
        Task<ReferenceInfoDto> Create(ReferenceInfoDto item);
    }
}