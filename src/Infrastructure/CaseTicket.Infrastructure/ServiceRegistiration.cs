using CaseTicket.Infrastructure.Cache.Redis;
using CaseTicket.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisCacheServiceSetting>(configuration.GetSection("RedisCacheServiceSetting"));
            services.AddStackExchangeRedisCache(op =>
            {
                op.Configuration = configuration["RedisCacheServiceSetting:ConnectionString"];
                op.InstanceName = configuration["RedisCacheServiceSetting:InstanceName"];
            });
            services.AddScoped<IRedisCacheService,RedisCacheService>();
        }

    }
}
