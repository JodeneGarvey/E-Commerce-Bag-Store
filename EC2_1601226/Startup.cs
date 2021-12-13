using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EC2_1601226.Data;
using Microsoft.AspNetCore.Identity;
using EC2_1601226.Repository;
using EC2_1601226.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EC2_1601226
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        public Startup(IHostingEnvironment hostingEnvironment /*IConfiguration configuration*/)
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
           /* Configuration = configuration;*/
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<EC2_1601226Context>(options =>
                    options.UseSqlServer(_configurationRoot.GetConnectionString("EC2_1601226Context")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EC2_1601226Context>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp));

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IBagRepository, BagRepository>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddMemoryCache();
            services.AddSession();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
            
        }
    }
}
