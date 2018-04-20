using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHubWeb.Configuration.ConfigServices;
using DevHubWeb.Configuration.Helpers;
using DevHubWeb.Domains;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DevHubWeb.API
{
    public class Startup
    {
        private static string _dbConnection;
        private static IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var dalfolder = PathHelper.GetParentFolder(env);

            var builder =
                new ConfigurationBuilder().SetBasePath(dalfolder)
                    .AddJsonFile($"PublishSettings\\appSettings.{env.EnvironmentName}.json", false, true);
            _config = builder.Build();
            _dbConnection = _config.GetConnectionString("DevHubDBConn");            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.DRConfigServices(_dbConnection);
            
            services.AddMvc();
            services.RegisterSwagger();
            services.AddAuthenticationService(_config.GetSection("AuthSettings").GetSection("JwtKey").Value);
            services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));
            services.Configure<AppSettingsModel>(_config.GetSection("SwaggerAuthentication"));
            services.Configure<AppSettingsModel>(_config.GetSection("ConnectionStrings"));
            services.Configure<AppSettingsModel>(_config.GetSection("AuthSettings"));
            services.Configure<AppSettingsModel>(_config.GetSection("Email"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                            .AllowCredentials());
            app.UseSwaggerAuthentication();
            app.UseAuthentication();
            app.UseMvc();
            app.RegisterSwagger();
            
        }
    }
}
