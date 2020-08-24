using ShortenedReferenceBLL.ModelDtos;
using ShortenedReferenceDAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Mappers
{
    public static class Mapper
    {
        public static  ReferenceInfo MapToDbModel(this ReferenceInfoDto referenceDto)
        {
            return referenceDto == null ?
                null :
                new  ReferenceInfo()
                {
                    Id = referenceDto.Id,
                    LongReference = referenceDto.LongReference,
                    ShortenedReference = referenceDto.ShortenedReference,
                    CreatedData = referenceDto.CreatedData,
                    CountTransitions = referenceDto.CountTransitions
                };
        }

        public static ReferenceInfoDto MapToDtoModel(this ReferenceInfo reference)
        {
            return reference == null ?
                null :
                new ReferenceInfoDto()
                {
                    Id = reference.Id,
                    LongReference = reference.LongReference,
                    ShortenedReference = reference.ShortenedReference,
                    CreatedData = reference.CreatedData,
                    CountTransitions = reference.CountTransitions
                };
        }

        public static IEnumerable<ReferenceInfoDto> MapToListDtoModels(this IEnumerable<ReferenceInfo> referenceInfos)
        {
            return referenceInfos.Select(item => item.MapToDtoModel());
        }

        public static IEnumerable<ReferenceInfo> MapToListDbModels(this IEnumerable<ReferenceInfoDto> referenceInfoDtos)
        {
            return referenceInfoDtos.Select(item => item.MapToDbModel());
        }
    }
}
