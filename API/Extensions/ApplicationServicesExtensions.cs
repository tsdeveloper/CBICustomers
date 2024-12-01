using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Addresses;
using Core.Interfaces.Services.Clients;
using Infra.Data;
using Infra.Repository;
using Infra.Repository.Addresses;
using Infra.Repository.Clients;
using Infra.Services;
using Infra.Services.Addresss;
using Infra.Services.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            var IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            var connectionString = IsDevelopment
                     ? config.GetConnectionString("DEV-DOCKER-SQLSERVER") :
                     config.GetConnectionString("PRD-DOCKER-SQLSERVER");
         
                     services.AddDbContext<CBICustomersContext>(opt =>
                     {
                         opt.UseSqlServer(connectionString, o => o.MigrationsAssembly("Infra"));
                     });

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}