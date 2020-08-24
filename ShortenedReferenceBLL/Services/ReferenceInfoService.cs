using System;
using ShortenedReferenceBLL.Interfaces;
using System.Threading.Tasks;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShortenedReferenceBLL.Services
{
    public class ReferenceInfoService : IReferenceInfoService<ReferenceInfo>
    {
        private static readonly int LENGTH_URL = 7;
        const string  CHARS= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";        

        private readonly Random random = new Random();
        private readonly IReferenceInfoRepository<ReferenceInfo> _referenceInfoRepository;

        public ReferenceInfoService(IReferenceInfoRepository<ReferenceInfo> referenceInfoRepository)
        {           
            _referenceInfoRepository = referenceInfoRepository;
        }

        public async Task<ReferenceInfo> Create(ReferenceInfo referenceInfo)
        {
            var referenceInDB = await _referenceInfoRepository.Find(referenceInfo.LongReference);
            if(referenceInDB != null)
            {
                return referenceInDB;
            }

            referenceInfo.ShortenedReference = Generate();

            return await _referenceInfoRepository.Create(referenceInfo);
        }

        public async Task Remove(int id)
        {
            await _referenceInfoRepository.Remove(id);
        }

        public Task<List<ReferenceInfo>> GetAll()
        {
            return _referenceInfoRepository.GetAll();
        }

        public Task<ReferenceInfo> Find(string url, bool isLongReference = true)
        {
            return _referenceInfoRepository.Find(url, isLongReference);
        }

        public Task<ReferenceInfo> Get(int id)
        {
            return _referenceInfoRepository.Get(id);
        }

        private string Generate()
        {
            string shortenedReference;
            do
            {
                shortenedReference = new string(Enumerable.Repeat(CHARS, LENGTH_URL)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (!ValidateShortReference(shortenedReference));

            return shortenedReference;
        }

        private bool ValidateShortReference(string shortReference)
        {
            if(shortReference == null)
            {
                return false;
            }

            List<ReferenceInfo> collectionReferenceInfo = _referenceInfoRepository.GetAll().Result;
            ReferenceInfo alignment = collectionReferenceInfo.Find(item => item.ShortenedReference.Equals(shortReference));

            if (alignment != null)
            {
                return false;
            }

            return true;
        }
    }
}