

using Hosptial.BLL.Common;
using Hosptial.BLL.Profiles;
using Hosptital.DAL.Common;
using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Claims;
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
            

            builder.Services.AddAuthentication(configOptions =>
            {
                configOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization();

            #region Configur DataBase
            builder.Services.AddDbContext<HospitalDbContext>(options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));

              });
            builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole<int>>()  // keep roles
    .AddEntityFrameworkStores<HospitalDbContext>()
    .AddDefaultTokenProviders();
            #endregion

            AddDALService.AddDAL(builder.Services);
            AddBLLService.AddBLL(builder.Services);



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
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
