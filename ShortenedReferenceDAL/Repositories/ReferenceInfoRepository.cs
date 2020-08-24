using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.DataBase;
using ShortenedReferenceDAL.Interfaces;

namespace ShortenedReferenceDAL.Repositories
{
    public class ReferenceInfoRepository : IReferenceInfoRepository<ReferenceInfo>
    {
        private readonly ReferenceShortenerContext _context;

        public ReferenceInfoRepository(ReferenceShortenerContext context)
        {
            _context = context;
        }

        public async Task<ReferenceInfo> Create(ReferenceInfo item)
        {
            var entity = await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (entity == null)
            {
                item.CreatedData = DateTime.Now;
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async Task<ReferenceInfo> Find(string url, bool isLongReference)
        {
            return isLongReference ? await _context.ReferenceInfos.Include(x => x.Counter).FirstOrDefaultAsync(x => x.LongReference == url) : 
                                     await _context.ReferenceInfos.Include(x => x.Counter).FirstOrDefaultAsync(x => x.ShortenedReference == url);
        }

        public async Task<ReferenceInfo> Get(int id)
        {
            return await _context.ReferenceInfos.Include(x => x.Counter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ReferenceInfo>> GetAll()
        {
            return await _context.ReferenceInfos.Include(x => x.Counter).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _context.ReferenceInfos.Include(x => x.Counter).FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _context.ReferenceInfos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}