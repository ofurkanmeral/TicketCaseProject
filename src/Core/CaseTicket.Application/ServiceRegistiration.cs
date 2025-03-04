using CaseTicket.Application.Interfaces;
using CaseTicket.Application.Services;
using CaseTicket.Application.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IObiletService,OBiletServices>();
            //services.AddHttpClient<HttpClientWrapper>();
        }

    }
}
