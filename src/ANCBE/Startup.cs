using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ANCBE
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ANCBE.Models.BlogDataContext>();
            services.AddScoped<ANCBE.Models.Identity.IdentityDataContext>();
            services.AddTransient<ANCBE.Models.FormattingService>();

            string identityConnectionString =
                @"Server=(LocalDb)\MSSQLLocalDb;Database=ANCBE_Identity";

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<Models.Identity.IdentityDataContext>(dbConfig =>
                    dbConfig.UseSqlServer(identityConnectionString));

            services.AddIdentity<Models.Identity.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Models.Identity.IdentityDataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                //app.UseBrowserLink();

                //var context = app.ApplicationServices.GetService<Models.BlogDataContext>();
                //context.Database.EnsureDeleted();
                //System.Threading.Thread.Sleep(1000);
                //context.Database.EnsureCreated();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            builder.AddEnvironmentVariables();

            var config = builder.Build();

            app.UseIdentity();

            app.UseFileServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
