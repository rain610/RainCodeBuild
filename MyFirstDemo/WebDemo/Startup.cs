using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Common;

namespace WebDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions()
                .Configure<MessageQueueOptions>(Configuration.GetSection("messageQueue"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                });
            services.AddSession();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<StatisticsService>();
            services.AddMvc();
            services
                .AddContextServices()
                .AddCommonServices();
            //services.AddAuthentication(options => 
            //{
            //    options.DefaultSignInScheme= CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme= CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie();

            //services.AddAuthentication("FiverSecurityScheme")
            //.AddCookie("FiverSecurityScheme", options =>
            // {
            //     //options.AccessDeniedPath = new PathString("/Security/Access");
            //     options.LoginPath = new PathString("/Login");
            //     options.Cookie.Path= "/schfltmgr";
            // });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
            //    AddCookie(options=> 
            //    {
            //        options.LoginPath = new PathString("/Login");
            //        //ptions.Cookie.Path= "/schfltmgr";
            //    });
            //services.AddAuthorization(config => 
            //{
            //    config.AddPolicy("aa", policy=>
            //    {
            //        policy.RequireRole("1");
            //    });
            //});
            //services.AddMvc(options=> 
            //{
            //    options.Filters.Add<AuthorizationFilters>();
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase("/schfltmgr");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
