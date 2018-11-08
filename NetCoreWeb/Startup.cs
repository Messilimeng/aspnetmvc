using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using Controllers.Attribute;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NetCoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static IContainer AutofacContainer;
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册服务进 IServiceCollection

            #region Session store

            // services.AddDistributedMemoryCache();

            var redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Test-Keys");
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("Redis");
                //option.InstanceName = "master";
            });

            services.AddSession(options =>
            {
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // https
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "sessionid";
            });
            // services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(3000); });
            #endregion

            #region Cookie store
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(UserAuthorizeAttribute.UserAuthenticationScheme, o =>
                {
                    o.Cookie.Path = "/";
                    o.Cookie.Name = "my.web.cookie";
                    o.LoginPath = "/WebSite/Login";
                    o.LogoutPath = "/WebSite/Logout";
                    o.Cookie.SecurePolicy = CookieSecurePolicy.None;
                });

            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DefaultModuleRegister>();
            AutofacContainer = builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
        }

        public IConfiguration Configuration { get; }

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
            }
            app.UseSession();
            app.UseStaticFiles();
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