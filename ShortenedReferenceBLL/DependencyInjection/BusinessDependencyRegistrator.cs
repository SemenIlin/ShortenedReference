using Microsoft.Extensions.DependencyInjection;
using ShortenedReferenceBLL.Interfaces;
using ShortenedReferenceBLL.Services;
using ShortenedReferenceCommon.Model;

namespace ShortenedReferenceBLL.DependencyInjection
{
    public static class BusinessDependencyRegistrator
    {
        public static void RegisterBusinessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReferenceInfoService<ReferenceInfo>, ReferenceInfoService>();

            serviceCollection.AddScoped<ICounterService<Counter>, CounterService>();
        }
    }
}