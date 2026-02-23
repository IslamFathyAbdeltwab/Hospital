using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Common
{
    public static class AddDALService
    {
        public static void AddDAL(this IServiceCollection services)
        {
            services.AddScoped<IDbInitlizer, DbInitlizer>();

        }
    }
}
