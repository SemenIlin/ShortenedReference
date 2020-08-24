using ShortenedReferenceBLL.ModelDtos;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Mappers
{
    public static class MapperUI
    {
        public static ReferenceInfoViewModel MapToViewModel(this ReferenceInfoDto referenceDto)
        {
            return referenceDto == null ?
                null :
                new ReferenceInfoViewModel()
                {
                    Id = referenceDto.Id,
                    LongReference = referenceDto.LongReference,
                    ShortenedReference = referenceDto.ShortenedReference,
                    CreatedData = referenceDto.CreatedData,
                    CountTransitions = referenceDto.CountTransitions
                };
        }

        public static ReferenceInfoDto MapToDtoModel(this ReferenceInfoViewModel referenceViewModel)
        {
            return referenceViewModel == null ?
                null :
                new ReferenceInfoDto()
                {
                    Id = referenceViewModel.Id,
                    LongReference = referenceViewModel.LongReference,
                    ShortenedReference = referenceViewModel.ShortenedReference,
                    CreatedData = referenceViewModel.CreatedData,
                    CountTransitions = referenceViewModel.CountTransitions
                };
        }

        public static IEnumerable<ReferenceInfoDto> MapToListDtoModels(this IEnumerable<ReferenceInfoViewModel> referenceInfoViewModels)
        {
            return referenceInfoViewModels.Select(item => item.MapToDtoModel());
        }

        public static IEnumerable<ReferenceInfoViewModel> MapToListViewModels(this IEnumerable<ReferenceInfoDto> referenceInfoDtos)
        {
            return referenceInfoDtos.Select(item => item.MapToViewModel());
        }
    }
}
