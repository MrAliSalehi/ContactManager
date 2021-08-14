using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webApi.Models.Entities;
using AppApi.DbManagement.Interfaces;
using AppApi.DbManagement.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AppApi.Properties;
using AppApi.Jwt;

namespace AppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<contactContext>(op => { op.UseSqlServer("Data Source = QWxp\\SQL2019;Initial Catalog=ContactDB;Integrated Security=True"); });
            services.AddTransient<IUserRepository, UserRepository>();

            #region Cache Setup
            services.AddResponseCaching();
            services.AddMemoryCache();
            #endregion

            #region Jwt Setup
            SecretKeyGen newKey = new SecretKeyGen();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(setting =>
                {
                    setting.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuers = PublicSettings.ApiAddress,
                        IssuerSigningKey = newKey.SecretKey
                    };
                });
            #endregion

            #region Policy Setup
            services.AddCors(s =>
            {
                s.AddPolicy("PolicyCR",
                    build =>
                    {
                        build.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build();
                    });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("PolicyCR");

            app.UseRouting();

            #region My-Chances
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseAuthentication();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
