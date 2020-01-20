using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Biz.Api.Formatters;
using Newtonsoft.Json;

namespace Biz.Api
{
    public class Startup
    {
        private static readonly string SwaggerPrefix = "api/swagger";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                });
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new TextPlainInputFormatter());
                //options.RespectBrowserAcceptHeader = false;
                //options.ReturnHttpNotAcceptable = true;
                //options.OutputFormatter.Add(new JsonOutputF)
            })
            .AddNewtonsoftJson(s => 
            {
                s.SerializerSettings.Formatting = Formatting.None;
                s.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "my little api", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Biz.Api.xml");
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger(o => o.RouteTemplate = SwaggerPrefix + "/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{SwaggerPrefix}/v1/swagger.json", "My API V1");
                c.RoutePrefix = SwaggerPrefix;
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
