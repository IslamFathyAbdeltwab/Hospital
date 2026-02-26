

using Hosptial.BLL.Profiles;
using Hosptital.DAL.Common;
using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hospital
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region Configur DataBase
            builder.Services.AddDbContext<HospitalDbContext>(options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));

              });
            builder.Services
              .AddIdentity<ApplicationUser, IdentityRole<int>>()
              .AddRoles<IdentityRole<int>>()
              .AddEntityFrameworkStores<HospitalDbContext>()
              .AddDefaultTokenProviders();
            #endregion

            AddDALService.AddDAL(builder.Services);



            var app = builder.Build();

            #region Configur Services
            using var scope = app.Services.CreateScope();
            var dbInitlizer = scope.ServiceProvider.GetRequiredService<IDbInitlizer>();
            await dbInitlizer.Initialize();


            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
