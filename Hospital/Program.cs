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

            // =========================
            // Add Controllers + OpenAPI .
            // =========================
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            //stripe 
            Stripe.StripeConfiguration.ApiKey =    builder.Configuration["Stripe:SecretKey"];
            // =========================
            // CORS CONFIGURATION (FIX)
            // =========================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // =========================
            // JWT AUTHENTICATION
            // =========================
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
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            // =========================
            // DATABASE CONFIGURATION
            // =========================
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
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultTokenProviders();

            // =========================
            // DAL + BLL SERVICES
            // =========================
            AddDALService.AddDAL(builder.Services);
            AddBLLService.AddBLL(builder.Services);

            var app = builder.Build();

            // =========================
            // DB INITIALIZER
            // =========================
            using (var scope = app.Services.CreateScope())
            {
                var dbInitlizer = scope.ServiceProvider.GetRequiredService<IDbInitlizer>();
                await dbInitlizer.Initialize();
            }

            // =========================
            // HTTP PIPELINE
            // =========================
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            // ? IMPORTANT: CORS must be here
            app.UseCors("AllowAngularDev");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}