using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Services;
using ZeiBook.Areas.Admin.Actions.Books;
using ZeiBook.Actions.Book;
using ZeiBook.Actions.Home;
using ZeiBook.Actions.Components;
using ZeiBook.Actions.Search;
using ZeiBook.Actions.Rank;
using ZeiBook.Actions.Comment;
using ZeiBook.Areas.Admin.Actions.Roles;

namespace ZeiBook
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o=> {
                var p = o.Password;
                p.RequireDigit = false;
                p.RequiredLength = 6;
                p.RequireLowercase = false;
                p.RequireNonAlphanumeric = false;
                p.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //
            services.AddTransient<CreateAction>();
            services.AddTransient<CategoryIndexAction>();
            services.AddTransient<Actions.Home.IndexAction>();
            services.AddTransient<Areas.Admin.Actions.Books.IndexAction>();
            services.AddTransient<Areas.Admin.Actions.Authors.IndexAction>();
            services.AddTransient<CategoryAction>();
            services.AddTransient<AuthorIndexAction>();
            services.AddTransient<GenderIndexAction>();
            services.AddTransient<BookIndexAction>();
            services.AddTransient<DownloadAction>();
            services.AddTransient<SearchAction>();
            services.AddTransient<RankAction>();
            services.AddTransient<CommentAction>();
            services.AddTransient<RoleIndexAction>();
            services.AddTransient<RoleConfigAction>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Browser Link is not compatible with Kestrel 1.1.0
                // For details on enabling Browser Link, see https://go.microsoft.com/fwlink/?linkid=840936
                // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
