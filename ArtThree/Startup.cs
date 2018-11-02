using System;
using System.Collections.Generic;
using System.IO;
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
                .AddJsonFile("appsettings.json");
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
            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

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
                    options.AccessDeniedPath = "/Home/Forbidden";
                   // options.LoginPath = "/Account/Login";
                    options.Cookie.Expiration = new TimeSpan(7, 0, 0, 0);
                });
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "ArtThree_AUTH";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            // Middleware to handle all request
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("/index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            app.UseFileServer(enableDirectoryBrowsing: false);
            app.UseMvc();
        }
    }
}
