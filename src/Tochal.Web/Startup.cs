using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Alamut.Service.Messaging.Contracts;
using Alamut.Service.Messaging.Implementation;
using Alamut.Service.Messaging.Configration;
using Alamut.Utilities.Http;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using Tochal.Core.Common.WebToolkit;
using Tochal.Core.DomainModels.ViewModel.Identity.Settings;
using Tochal.Infrastructure.DataLayer.Context; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IocConfig;
using Tochal.Infrastructure.Services;
using Tochal.Infrastructure.Services.Routine2;
using Tochal.Core.DomainModels.SSOT;
using AutoMapper;
using Tochal.Infrastructure.Mapping;
using Tochal.Infrastructure.Services.Contracts.Genericservice;
using Tochal.Infrastructure.Services.GenericService;
using Tochal.Infrastructure.Services.Contracts;

namespace Tochal.Web
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

            services.AddMvc(options =>
            {
                options.UseYeKeModelBinder();
                options.AllowEmptyInputInBodyModelBinding = true;
                // options.Filters.Add(new NoBrowserCacheAttribute());
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
          .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddDNTCommonWeb();
            services.AddDNTCaptcha();
            services.AddCloudscribePagination();

            //services.AddAutoMapper(typeof(IdentityMappings).GetTypeInfo().Assembly);

            services.AddScoped<IUserResolverService, UserResolverServiceByHttpContext>();
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            //// Infrastructure Services
            //services.AddSingleton<Service.Messaging.Contracts.IEmailService,
            //    MimeKitEmailServices>();
            //services.AddSingleton<ISmsService, ResearchSmsServices>();
             
            services.Configure<SmsConfig>(options => Configuration.Bind(options)); 
             
            services.Configure<SiteSettings>(options => Configuration.Bind(options));

            //services.AddPoco<ContactInfo>(Configuration.Bind(options));

             services.AddScoped<FileConfig>();
            services.Configure<FileConfig>(Configuration.GetSection("FileConfig"));
            
             services.AddScoped<ContactInfo>();
            services.Configure<ContactInfo>(Configuration.GetSection("ContactInfo"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAutoMapper(typeof(Routine2Mapping).GetTypeInfo().Assembly);

            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<ISmsManagement, SmsManagement>();

            services.AddSingleton<ISmsService, ResearchSmsServices>();
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

            //if (env.IsDevelopment())
            //{
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/error/index/500");
            //    app.UseStatusCodePagesWithReExecute("/error/index/{0}");

            //}

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
