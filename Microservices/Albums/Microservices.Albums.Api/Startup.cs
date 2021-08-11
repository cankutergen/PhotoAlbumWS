using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microservices.Albums.Api.Models;
using Microservices.Albums.Business;
using Microservices.Albums.Business.ValidationRules.FluentValidaton;
using Microservices.Albums.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhotoAlbumWS.Core.REST;
using PhotoAlbumWS.Core.REST.Abstract;
using PhotoAlbumWS.Core.REST.Concrete;
using RestSharp;

namespace Microservices.Albums.Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Album", Version = "v1" });
            });

            services.AddSingleton<IApiService<Album>>(x =>
                ActivatorUtilities.CreateInstance<ApiService<Album>>(x, Configuration.GetSection("AlbumsApiBase").Value)
            );

            services.AddSingleton<IApiService<UserModel>>(x =>
                ActivatorUtilities.CreateInstance<ApiService<UserModel>>(x, Configuration.GetSection("UsersApiBase").Value)
            );

            services.AddSingleton<IValidator<Album>, AlbumValidator>();

            services.AddMediatR(typeof(AlbumMediatrEntryPoint).Assembly);
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Album V1");
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
