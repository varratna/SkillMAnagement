using System;
using System.IO;
using System.Text;
using LoggingService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;
using SkillManagement.API.Core.Services;
using SkillManagement.API.Data.Repositories;
using SkillManagement.API.Extensions;

using SkillManagement.API.Services;
using SkillManagement.API.ViewModel;

namespace SkillManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<SkillContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SkillManagementConnection"));
                //options.UseInMemoryDatabase("supermarket-api-in-memory");
            });

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                             .AllowAnyMethod()
                                                              .AllowAnyHeader()));




            //jwt token
            var appSettingsSection = Configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<ApplicationSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JWT_Secret);
            //call AddAuthentication
            //set the default authentication schemas
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })//configure jwt token 
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // restrict to  https only
                x.SaveToken = true; // to save on server
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, //to validate issuer 
                    ValidateAudience = false // to validate audience 
                };
            });
                                                  
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeSkillLevelRepository, EmployeeSkillLevelRepository>();

            services.AddSingleton<ILoggingService, LogService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IEmployeeSkillLevelService, EmployeeSkillLevelService>();
         //   services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggingService logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
