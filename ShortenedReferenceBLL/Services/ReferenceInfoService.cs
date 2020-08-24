using System;
using ShortenedReferenceBLL.Interfaces;
using System.Threading.Tasks;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShortenedReferenceBLL.Services
{
    public class ReferenceInfoService : IReferenceInfoService<ReferenceInfo>
    {
        private const string LOCAL_HOST = "http://localhost:44339/";
        private static readonly int LENGTH_URL = 7;
        private static readonly char[] LETTERS = new char[]
        {
            'q','Q','w','W','e','E','r','R','t','T','y','Y','u','U','i','I','o','O',
            'p','P','a','A','s','S','d','D','f','F','g','G','h','H','j','J','k','K',
            'l','L','z','Z','x','X','c','C','v','V','b','B','n','N','m','M'
        };

        private readonly Random random = new Random();
        private readonly IReferenceInfoRepository<ReferenceInfo> _referenceInfoRepository;

        private bool isDigit = false;

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
                shortenedReference = null;
                for (int i = 0; i < LENGTH_URL; i++)
                {
                    isDigit = random.Next(0, 2) == 1;
                    shortenedReference += GetSymbol(isDigit).ToString();
                }
            }
            while (!ValidateShortReference(shortenedReference));

            shortenedReference = LOCAL_HOST + shortenedReference;

            return shortenedReference;
        }

        private char GetSymbol(bool isDigit)
        {
            return isDigit ? (char)('0' + random.Next(0, 10)) :
                       LETTERS[random.Next(0, LETTERS.Length)];
        }

        private bool ValidateShortReference(string shortReference)
        {
            if(shortReference == null)
            {
                return false;
            }

            Regex regexForShortUrl = new Regex("[a-zA-Z0-9]");

            if (regexForShortUrl.Matches(shortReference).Count != LENGTH_URL)
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