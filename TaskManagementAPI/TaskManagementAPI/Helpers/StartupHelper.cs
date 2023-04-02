﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Helpers
{
    public static class StartupHelper
    {
        public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                       .UseSnakeCaseNamingConvention();
            });

        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                   };
               });
        }

        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
        }
    }
}
