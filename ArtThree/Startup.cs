using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtThree.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtThree
{
    public class Startup
    {
        public Startup(IHostingEnvironment p_hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(p_hostingEnvironment.ContentRootPath)
                .AddJsonFile("config.json");
            Configuration = builder.Build();
            HostingEnvironment = p_hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            PlatformID platform = Environment.OSVersion.Platform;
            if (platform != PlatformID.Win32NT)
            {
            }
            else
            {
                var connection = Configuration["Database:SqlServerConnection"];
                services.AddDbContext<ATDbContext>(options => options.UseSqlServer(connection));
            }

            services.AddSingleton(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddTransient<IATRepository, ATRepository>();
         


            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddAuthentication("ST_AUTH")
                .AddCookie("ST_AUTH", options =>
                {
                    options.AccessDeniedPath = "/Account/Forbidden";
                    options.LoginPath = "/Account/Login";
                    options.Cookie.Expiration = new TimeSpan(7, 0, 0, 0);
                });
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "ST_AUTH";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
