using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;
using Tesseract.Repository.Context;
using Tesseract.Repository.Repository;
using Tesseract.Service.Services;

namespace Tesseract.Api
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddScoped<IExtractionRepository, ExtractionRepository>();
            services.AddScoped<IBaseRepository<ExtractionEntity>, BaseRepository<ExtractionEntity>>();
            services.AddScoped<IBaseService<ExtractionEntity>, BaseService<ExtractionEntity>>();
            services.AddScoped<ITesseractService, TesseractService>();

            services.AddDbContext<OcrContext>(options => options.UseSqlServer(_configuration.GetSection("ConnectionString").Value));

            services.AddControllers()
                    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tesseract.Api - OCR",
                    Description = "API para OCR",
                    Contact = new OpenApiContact
                    {
                        Name = "Teste",
                        Email = "admin@teste.com.br",
                        Url = new Uri("http://www.teste.com.br/"),
                    }

                });
            });

            ConfigureCors(services);

        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(new string[] { "*.heroku.com.br" })
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .AllowAnyHeader();
                });

                option.AddPolicy("LocalCorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true)
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .AllowAnyHeader();
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseCors("LocalCorsPolicy");

            //app.UseAzureAdAuthConfiguration();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "tesseractOCR");
                options.RoutePrefix = "swagger";
            });

            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            //app.UseAuthorization();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

        }
    }
}
