using System;
using System.IO;
using LoggingService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using SkillManagement.API.Core.Repositories;
using SkillManagement.API.Core.Services;
using SkillManagement.API.Data.Repositories;
using SkillManagement.API.Extensions;

using SkillManagement.API.Services;

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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserSkillLevelRepository, UserSkillLevelRepository>();

            services.AddSingleton<ILoggingService, LogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IUserSkillLevelService, UserSkillLevelService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
