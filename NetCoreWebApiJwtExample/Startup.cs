using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.Business.Concrete;
using NetCoreWebApiJwtExample.DataAccess.Abstract;
using NetCoreWebApiJwtExample.DataAccess.Concrete.EntityFrameworkCore;
using NetCoreWebApiJwtExample.Helpers;
using NetCoreWebApiJwtExample.JwtIdentity;

namespace NetCoreWebApiJwtExample
{
    public class Startup
    {
        //public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Atacaðýmýz isteklerde CORS problemi yaþamamak için:
            services.AddCors();
            services.AddControllers();
            //Projemizde DbContext olarak ApplicationDbContext kullanacaðýmýz belirtliyoruz.
            services.AddDbContext<ApplicationDbContext>();
            // appsettings.json içinde oluþturduðumuz gizli anahtarýmýzý AppSettings ile çaðýracaðýmýzý söylüyoruz.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Oluþturduðumuz gizli anahtarýmýzý byte dizisi olarak alýyoruz.
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            //Projede farklý authentication tipleri olabileceði için varsayýlan olarak JWT ile kontrol edeceðimizin bilgisini kaydediyoruz.
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //JWT kullanacaðým ve ayarlarý da þunlar olsun dediðimiz yer ise burasýdýr.
                .AddJwtBearer(x =>
                {
                    //Gelen isteklerin sadece HTTPS yani SSL sertifikasý olanlarý kabul etmesi(varsayýlan true)
                    x.RequireHttpsMetadata = false;
                    //Eðer token onaylanmýþ ise sunucu tarafýnda kayýt edilir.
                    x.SaveToken = true;
                    //Token içinde neleri kontrol edeceðimizin ayarlarý.
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Token 3.kýsým(imza) kontrolü
                        ValidateIssuerSigningKey = true,
                        //Neyle kontrol etmesi gerektigi
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //DI için IUserService arayüzünü çaðýrdýðým zaman UserService sýnýfýný getirmesini söylüyorum.
            services.AddScoped<IUserService, UserManagerr>();

            services.AddScoped<IEmployeeService, EmployeeManager>();
            services.AddScoped<IEmployeeDal, EfCoreEmployeeDal>();

            services.AddScoped<IPersonService, PersonManager>();
            services.AddScoped<IPersonDal, EfCoreTaskPersonDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //CORS için hangi ayarlarý kullanacaðýmýzý belirtiyoruz.
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //Son olarak authentication kullanacaðýmýzý belirtiyoruz.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

