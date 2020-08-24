using Microsoft.EntityFrameworkCore;
using ShortenedReferenceDAL.Models;

namespace ShortenedReferenceDAL.DataBase
{
    public class ReferenceShortenerContext : DbContext
    {
        public DbSet<ReferenceInfo> ReferenceInfos { get; set; }

        public ReferenceShortenerContext(DbContextOptions<ReferenceShortenerContext> options)
            : base(options)
        {
        }
    }
}