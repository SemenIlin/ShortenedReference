﻿using Microsoft.Extensions.DependencyInjection;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.Interfaces;
using ShortenedReferenceDAL.Repositories;

namespace ShortenedReferenceDAL.DependencyInjection
{
    public static class DataDependencyRegistrator
    {
        public static void RegisterDataRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReferenceInfoRepository<ReferenceInfo>, ReferenceInfoRepository>();

            serviceCollection.AddScoped<ICounterRepository<Counter>, CounterRepository>();
        }
    }
}