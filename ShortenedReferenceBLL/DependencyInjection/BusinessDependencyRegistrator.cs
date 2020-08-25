using Microsoft.Extensions.DependencyInjection;
using ShortenedReferenceBLL.Interfaces;
using ShortenedReferenceBLL.ModelDtos;
using ShortenedReferenceBLL.Services;

namespace ShortenedReferenceBLL.DependencyInjection
{
    public static class BusinessDependencyRegistrator
    {
        public static void RegisterBusinessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReferenceInfoService<ReferenceInfoDto>, ReferenceInfoService>();
        }
    }
}