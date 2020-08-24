using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortenedReferenceDAL.DataBase;
using ShortenedReferenceDAL.Interfaces;
using ShortenedReferenceDAL.Models;

namespace ShortenedReferenceDAL.Repositories
{
    public class ReferenceInfoRepository : IReferenceInfoRepository
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
            return isLongReference ? await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.LongReference == url) : 
                                     await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.ShortenedReference == url);
        }

        public async Task<ReferenceInfo> Get(int id)
        {
            return await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ReferenceInfo>> GetAll()
        {
            return await _context.ReferenceInfos.ToListAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _context.ReferenceInfos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(int id)
        {
            var entity = await _context.ReferenceInfos.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                entity.CountTransitions++;
                await _context.SaveChangesAsync();
            }
        }
    }
}