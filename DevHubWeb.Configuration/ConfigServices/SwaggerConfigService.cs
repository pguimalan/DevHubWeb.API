using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Configuration.ConfigServices
{
    public static class SwaggerConfigService
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {

            return services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "DevHub API Test Environment",
                    Version = "March 13, 2018"
                });
            });
        }

        public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "DevHub API v1");
                c.RoutePrefix = "docs/swagger";
            });

            return applicationBuilder;
        }
    }
}
