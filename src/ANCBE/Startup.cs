using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ANCBE
{
    public class Startup
    {
        private readonly IConfigurationRoot config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .AddEnvironmentVariables()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ANCBE.Models.BlogDataContext>();
            services.AddScoped<ANCBE.Models.Identity.IdentityDataContext>();
            services.AddTransient<ANCBE.Models.FormattingService>();

            var connectionString = config.GetConnectionString("BlogDataContext");
            var identityConnectionString = config.GetConnectionString("IdentityDataContext");

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<Models.BlogDataContext>(dbConfig =>
                    dbConfig.UseSqlServer(connectionString))
                .AddDbContext<Models.Identity.IdentityDataContext>(dbConfig =>
                    dbConfig.UseSqlServer(identityConnectionString));

            services.AddIdentity<Models.Identity.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Models.Identity.IdentityDataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                if (config.GetValue<bool>("RecreateDatabase:BlogData"))
                {
                    var context = app.ApplicationServices.GetService<Models.BlogDataContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
                if (config.GetValue<bool>("RecreateDatabase:IdentityData"))
                {
                    var contextIdentity = app.ApplicationServices.GetService<Models.Identity.IdentityDataContext>();
                    contextIdentity.Database.EnsureDeleted();
                    contextIdentity.Database.EnsureCreated();
                }
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseIdentity();

            app.UseFileServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
