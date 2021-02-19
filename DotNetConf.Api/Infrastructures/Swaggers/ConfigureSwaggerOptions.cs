﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace DotNetConf.Api.Infrastructures.Swaggers
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Dotnet Konf 2021 Restful API Design",
                Version = description.ApiVersion.ToString(),
                Description = "Dotnet Konf 2021 Restful API Design Swagger Description.",
                Contact = new OpenApiContact()
                {
                    Name = "Berk Emre Çabuk",
                    Email = "berkemrecabuk@gmail.com",
                    Url = new Uri("https://emrecabuk.com/")
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Bu Version kullanımdan kaldırılmıştır.";
            }

            return info;
        }
    }
}
