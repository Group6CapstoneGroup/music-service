using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MusicService.Repositories;
using MusicService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicService", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            ConfigureMusicService(services);
            ConfigureSelectedSongService(services);
            ConfigureDbContext(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var supportedCultures = new[] { "en-US" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicService v1"));


            app.UseRouting();
            //music-service requirment 1.1.0 This helps allow UI to ping music service by enabling CORS (Cross origin resource sharing).
            app.UseCors(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<Repositories.Common.MusicDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("MusicDb"), npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                });
            });
        }

        private void ConfigureMusicService(IServiceCollection services)
        {
            services.AddScoped<IMusicRepository, MusicRepository>();
            services.AddTransient<IMusicService, SongService>();
            services.AddAutoMapper(typeof(ModelMapping.MusicProfile));
        }

        private void ConfigureSelectedSongService(IServiceCollection services)
        {
            services.AddScoped<ISelectedSongRepository, SelectedSongRepository>();
            services.AddTransient<ISelectedSongService, SelectedSongService>();
            services.AddAutoMapper(typeof(ModelMapping.SelectedSongProfile));
        }
    }
}
