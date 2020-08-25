using Microsoft.Extensions.DependencyInjection;
using ShortenedReferenceDAL.Interfaces;
using ShortenedReferenceDAL.Models;
using ShortenedReferenceDAL.Repositories;

namespace ShortenedReferenceDAL.DependencyInjection
{
    public static class DataDependencyRegistrator
    {
        public static void RegisterDataRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReferenceInfoRepository<ReferenceInfo>, ReferenceInfoRepository>();
        }
    }
}