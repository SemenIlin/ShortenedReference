using System;
using ShortenedReferenceBLL.Interfaces;
using System.Threading.Tasks;
using ShortenedReferenceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ShortenedReferenceBLL.ModelDtos;
using ShortenedReferenceBLL.Mappers;
using ShortenedReferenceDAL.Models;

namespace ShortenedReferenceBLL.Services
{
    public class ReferenceInfoService : IReferenceInfoService<ReferenceInfoDto>
    {
        private static readonly int LENGTH_URL = 7;
        const string  CHARS= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";        

        private readonly Random random = new Random();
        private readonly IReferenceInfoRepository<ReferenceInfo> _referenceInfoRepository;

        public ReferenceInfoService(IReferenceInfoRepository<ReferenceInfo> referenceInfoRepository)
        {           
            _referenceInfoRepository = referenceInfoRepository;
        }

        public async Task<ReferenceInfoDto> Create(ReferenceInfoDto referenceInfo)
        {
            bool isValidate = await ValidateReference(referenceInfo.LongReference);
            if (!isValidate)
            {
                return await Task.FromResult<ReferenceInfoDto>(null);
            }

            ReferenceInfo referenceInDB = await _referenceInfoRepository.Find(referenceInfo.LongReference);
            if(referenceInDB != null)
            {
                return referenceInDB.MapToDtoModel();
            }

            referenceInfo.ShortenedReference = await Generate();

            return (await _referenceInfoRepository.Create(referenceInfo.MapToDbModel())).MapToDtoModel();
        }

        public async Task Remove(int id)
        {
            await _referenceInfoRepository.Remove(id);
        }

        public async Task<List<ReferenceInfoDto>> GetAll()
        {
            return (await _referenceInfoRepository.GetAll()).MapToListDtoModels().ToList();
        }

        public async Task<ReferenceInfoDto> Find(string url, bool isLongReference = true)
        {
            return (await _referenceInfoRepository.Find(url, isLongReference)).MapToDtoModel();
        }

        public async Task<ReferenceInfoDto> Get(int id)
        {
            return (await _referenceInfoRepository.Get(id)).MapToDtoModel();
        }

        public async Task Update(int id)
        {
            await _referenceInfoRepository.Update(id); 
        }

        private async Task<string> Generate()
        {
            string shortenedReference;
            bool isSuccess; 
            do
            {
                shortenedReference =  new string(Enumerable.Repeat(CHARS, LENGTH_URL)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                isSuccess = await ValidateShortReference(shortenedReference);
            }
            while(!isSuccess);

            return shortenedReference;
        }

        private Task<bool> ValidateReference(string reference)
        {
            bool result = Uri.TryCreate(reference, UriKind.Absolute, out Uri uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return Task.FromResult(result);
        }

        private async Task<bool> ValidateShortReference(string reference)
        {
            if(reference == null)
            {
                return false;
            }

            ReferenceInfoDto myReference = (await _referenceInfoRepository.Find(reference, false)).MapToDtoModel();

            if (myReference != null)
            {
                return false;
            }

            return true;
        }
    }
}