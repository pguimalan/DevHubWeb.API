using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DevHubWeb.Data.Entities;
using DevHubWeb.Data.Repo;
using DevHubWeb.Data.Repo.Implementation;
using DevHubWeb.Service.Implementations;
using DevHubWeb.Service;
using Microsoft.AspNetCore.Builder;
using DevHubWeb.Configuration.Helpers;
using AutoMapper;
using DevHubWeb.Service.Methods;

namespace DevHubWeb.Configuration.ConfigServices
{
    public static class DependencyResolver
    {
        public static IServiceCollection DRConfigServices(this IServiceCollection services, string dbConnection)
        {
            //adding DB Dependecy Injection
            services.AddDbContext<DevHubContext>(options => options.UseSqlServer(dbConnection));
            
            //Adding repository DI 
            services.AddScoped<IAmenitiesRepository, AmenitiesRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPhotosRepository, PhotosRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IBookLogRepository, BookLogRepository>();
            services.AddScoped<ITimeTrackingRepository, TimeTrackingRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IEmailRecipientRepository, EmailRecipientRepository>();
            
            //Adding Service DI
            services.AddScoped<IAmenitiesService, AmenitiesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPhotosService, PhotosService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IBookLogService, BookLogService>();
            services.AddScoped<ITimeTrackingService, TimeTrackingService>();
            services.AddScoped<ITransactionService, TransactionService>();


            services.AddTransient<SendEmailService>();
            services.AddTransient<HttpResponseService>();
            
            services.AddAutoMapper();
            services.AddTransient<HttpResponseHelper>();

            return services;            
        }
    }
}
