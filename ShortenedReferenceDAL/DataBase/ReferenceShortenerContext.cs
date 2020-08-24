using Microsoft.EntityFrameworkCore;
using ShortenedReferenceCommon.Model;

namespace ShortenedReferenceDAL.DataBase
{
    public class ReferenceShortenerContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }
        public DbSet<ReferenceInfo> ReferenceInfos { get; set; }

        public ReferenceShortenerContext(DbContextOptions<ReferenceShortenerContext> options)
            : base(options)
        {
        }
    }
}