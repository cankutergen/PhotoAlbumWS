using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microservices.Photos.Business;
using Microservices.Photos.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhotoAlbumWS.Core.REST.Abstract;
using PhotoAlbumWS.Core.REST.Concrete;
using RestSharp;

namespace Microservices.Photos.Api
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
            services.AddControllers();

            services.AddMemoryCache();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Photo", Version = "v1" });
            });

            services.AddSingleton<IApiService<Photo>>(x =>
                ActivatorUtilities.CreateInstance<ApiService<Photo>>(x, Configuration.GetSection("PhotosApiBase").Value)
            );

            services.AddSingleton<IApiService<AlbumModel>>(x =>
                ActivatorUtilities.CreateInstance<ApiService<AlbumModel>>(x, Configuration.GetSection("AlbumsApiBase").Value)
            );

            services.AddMediatR(typeof(PhotoMediatrEntryPoint).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Photo V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
