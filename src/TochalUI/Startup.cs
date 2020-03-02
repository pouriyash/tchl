using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using IocConfig;
using IocConfig.Routine2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tochal.Core.Common.WebToolkit;
using Tochal.Core.DomainModels.Entity;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Infrastructure.Services;
using Tochal.Infrastructure.Services.Contracts.Genericservice;
using Tochal.Infrastructure.Services.GenericService;

namespace TochalUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();


        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddOptions();

            services.Configure<SiteSettings>(options => Configuration.Bind(options));
            services.AddCustomIdentityServices();
             
            var siteSettings = services.GetSiteSettings();
            services.AddRequiredEfInternalServices(siteSettings); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.

            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.SetDbContextOptions(siteSettings);
                optionsBuilder.UseInternalServiceProvider(serviceProvider); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            });
            services.AddScoped<IDbConnection>(
            _ => new SqlConnection(siteSettings.ConnectionStrings.SqlServer.ApplicationDbContextConnection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                   options => { options.ResourcesPath = "Resources"; });

            services.AddCloudscribePagination();

            //services.AddAutoMapper(typeof(IdentityMappings).GetTypeInfo().Assembly);

            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            //// Infrastructure Services
            //services.AddSingleton<Service.Messaging.Contracts.IEmailService,
            //    MimeKitEmailServices>();
            //services.AddSingleton<ISmsService, ResearchSmsServices>();

            services.AddScoped<ContactInfo>();
            services.AddScoped<FileConfig>();
            services.Configure<FileConfig>(Configuration.GetSection("FileConfig"));
            //services.Configure<FileConfig>(options => Configuration.Bind(options));
            services.AddSingleton<IConfiguration>(Configuration);

            var d = Configuration.GetSection("FileConfig");

            services.Configure<SiteSettings>(options => Configuration.Bind(options));

            //services.AddPoco<ContactInfo>(Configuration.Bind(options));

            services.AddAutoMapper(typeof(Routine2Mapping).GetTypeInfo().Assembly);

            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Domain Services
            services.Scan(scan =>
                scan.FromAssemblyOf<UserService>()
                    .AddClasses(classes => classes.InNamespaceOf<UserService>())
                    .AsSelf()
                    .WithScopedLifetime());

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/index/500");
                app.UseStatusCodePagesWithReExecute("/error/index/{0}");

            }
            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            var supportedCultures =new List<CultureInfo>()
            {
                new CultureInfo("fa-IR"),
                new CultureInfo("en-US")
            };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            };

            app.UseRequestLocalization(options);

            app.UseContentSecurityPolicy();

            // Serve wwwroot as root
            app.UseFileServer(new FileServerOptions
            {
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            app.UseAuthentication();
            // app.UseNoBrowserCache();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

            });
        }

    }
}
