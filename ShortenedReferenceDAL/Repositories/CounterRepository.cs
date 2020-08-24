using Microsoft.EntityFrameworkCore;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.DataBase;
using ShortenedReferenceDAL.Interfaces;
using System.Threading.Tasks;

namespace ShortenedReferenceDAL.Repositories
{
    public class CounterRepository : ICounterRepository<Counter>
    {
        private readonly ReferenceShortenerContext _context;

        public CounterRepository(ReferenceShortenerContext context)
        {
            _context = context;
        }

        public async Task<Counter> Get(int id)
        {
            return await _context.Counters.FirstOrDefaultAsync(x => x.ReferenceInfoId == id);
        }

        public async Task<Counter> Create(Counter item)
        {
            var entity = await _context.Counters.FirstOrDefaultAsync(x => x.ReferenceInfoId == item.ReferenceInfoId);

            if (entity == null)
            {
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async Task<Counter> Update(int id)
        {
            var entity = await _context.Counters.FirstOrDefaultAsync(x => x.ReferenceInfoId == id);

            if (entity != null)
            {
                entity.AmountClickLink++;

                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }
    }
}
