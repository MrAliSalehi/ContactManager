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
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
            )
                .AddJwtBearer(setting =>
                {
                    setting.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = con =>
                        {
                            var accesstoken = con.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                            if (!string.IsNullOrEmpty(accesstoken))
                            {
                                con.Token = accesstoken;
                            }
                            return Task.FromResult(true);
                        }
                    };
                    setting.RequireHttpsMetadata = false;
                    setting.SaveToken = true;
                    setting.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretKeyFromWebApiItsVerySecret")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            #endregion

            //#region Policy Setup
            //services.AddCors(s =>
            //{
            //    s.AddPolicy("PolicyCR",
            //        build =>
            //        {
            //            build.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .Build();
            //        });
            //});
            //#endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            // app.UseCors("PolicyCR");
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
