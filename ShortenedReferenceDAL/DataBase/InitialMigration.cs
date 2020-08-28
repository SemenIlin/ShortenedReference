using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ShortenedReferenceDAL.DataBase
{
    public static class InitialMigration
    {
        public static async Task EnsureDatabasesMigrated(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ReferenceShortenerContext > ())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }
    }
}