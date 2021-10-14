using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql.Logging;
using PlumsailTest.DAL.Sql;
using PlumsailTest.Logic.MvcFilters;
using PlumsailTest.Logic.Services;
using PlumsailTest.Logic.Services.Abstractions;

namespace PlumsailTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDataAccess(services);
            RegisterServices(services);
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAll", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true);
                });
            });
            
            services.AddControllers();
            services.AddMvcCore(options => options.Filters.Add(typeof(AppBadRequestExceptionFilter))).AddNewtonsoftJson();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "PlumsailTest", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlumsailTest v1"));
            }

            app.UseCors("CorsAll");
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private void ConfigureDataAccess(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetSection("Sql").GetValue<string>("ConnectionString");
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(sqlConnectionString));

#if DEBUG
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Info, true);
            NpgsqlLogManager.IsParameterLoggingEnabled = true;
#endif
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IFormService, FormService>();
        }
    }
}